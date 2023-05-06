using System;
using System.Collections;
using UnityEngine;

[RequireComponent(
    typeof(Rigidbody2D), 
    typeof(BoxCollider2D))
]
public class PlayerController : CharacterBasicController
{
    #region Singleton
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    [Header("Sticky Positions")] 
    [SerializeField] private GameObject stickyPositions;
    [SerializeField] private BoxCollider2D groundCheckBoxCastPosition;
    
    private float _horizontalInput;

    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.15f;
    private float jumpBufferCounter;
    
    public bool IsGrounded { get; private set; }

    private void Update()
    {
        CheckMovementInput();

        if (IsGroundedCheck())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0f)
        {
            Jump();
            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump"))
        {
            coyoteTimeCounter = 0f;
        }

        if (Input.GetButtonDown("SwitchUniverse"))
        {
            SwitchUniverse();
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveDelta = new Vector2(_horizontalInput, 0);
        Move(moveDelta);
    }

    private bool IsGroundedCheck() 
    {
        Bounds boxColliderBounds = groundCheckBoxCastPosition.bounds;
        /*RaycastHit2D raycastHit = Physics2D.Raycast(
            groundCheckRayCastPosition.position,
            Vector3.down,
            0.025f,
            GameLayersManager.Instance.groundLayerMask | GameLayersManager.Instance.grabbableObjectsLayerMask);*/
        RaycastHit2D raycastHit = BoxCastDrawer.BoxCastAndDraw(
            boxColliderBounds.center,
            boxColliderBounds.size,
            0,
            Vector3.down,
            0.025f,
            GameLayersManager.Instance.groundLayerMask | GameLayersManager.Instance.grabbableObjectsLayerMask);
        IsGrounded = (raycastHit.collider != null);
        return IsGrounded;
    }

    private void SwitchUniverse()
    {
        UniverseSwitchManager.Instance.SwitchUniverse();
    }

    private void CheckMovementInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        if (_horizontalInput > 0)
            stickyPositions.transform.eulerAngles = new Vector3(0, 0, 0);
        else if(_horizontalInput < 0)
            stickyPositions.transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
