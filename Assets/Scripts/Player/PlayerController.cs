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

    private float walkTimer = 0f;
    [SerializeField] private float stepInterval;

    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            PauseMenu.Instance.Toggle();
        }

        CheckMovementInput();
        bool isGrounded = IsGroundedCheck();

        walkTimer += Time.deltaTime;

        if (isGrounded && _horizontalInput != 0 && walkTimer >= stepInterval)
        {
            AudioManager.Instance.PlaySound("Footsteps");
            walkTimer = 0f;
        }

        if (isGrounded)
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
        bool previousIsGrounded = IsGrounded;
        IsGrounded = (raycastHit.collider != null);

        if (IsGrounded == true && previousIsGrounded == false) //landed
        {
            AudioManager.Instance.PlaySound("Land");
        }

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
