using System;
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

    [Header("Universe Variables")]
    [SerializeField] private Universe startingUniverse = Universe.None;

    [Header("Sticky Positions")] 
    [SerializeField] private GameObject stickyPositions;
    
    private float _horizontalInput;
    
    public override void Start()
    {
        base.Start();
        UniverseSwitchManager.Instance.SetUniverse(startingUniverse);
    }

    private void Update()
    {
        CheckMovementInput();

        if (Input.GetButtonDown("Jump") && CanJump())
        {
            Jump();
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

    private bool CanJump()
    {
        return IsGrounded();
    }

    private bool IsGrounded()
    {
        Bounds boxColliderBounds = BoxCollider.bounds;
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxColliderBounds.center,
            boxColliderBounds.size,
            0,
            Vector3.down,
            0.1f,
            GameLayersManager.Instance.groundLayerMask | GameLayersManager.Instance.grabbableObjectsLayerMask);
        return raycastHit.collider != null;
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
