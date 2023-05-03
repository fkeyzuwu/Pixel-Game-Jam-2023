using UnityEngine;

[RequireComponent(
    typeof(Rigidbody2D), 
    typeof(BoxCollider2D))
]
public class CharacterBasicController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float movementSpeed = 5.0f;

    [Header("Jump")] 
    [SerializeField] private float jumpForce = 16.0f;

    protected Rigidbody2D Rigidbody;
    protected BoxCollider2D BoxCollider;
    private float _initialMovementSpeed;
    private float _initialJumpForce;

    public Vector2 MoveDelta { get; protected set; }
    public bool IsWalking { get; protected set; }
    
    public virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        _initialMovementSpeed = movementSpeed;
        _initialJumpForce = jumpForce;
    }
    
    protected virtual void Move(Vector2 moveDelta)
    {
        IsWalking = moveDelta != Vector2.zero;
        MoveDelta = moveDelta;
        Rigidbody.velocity = new Vector2(moveDelta.x * movementSpeed, Rigidbody.velocity.y);
    }
    
    protected void Jump()
    {
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, jumpForce);
    }

    public void RestoreMovementSpeedToDefault()
    {
        SetMovementSpeed(_initialMovementSpeed);
    }
    
    public void RestoreJumpForceToDefault()
    {
        SetJumpForce(_initialJumpForce);
    }
    
    public void SetMovementSpeed(float newMovementSpeed)
    {
        movementSpeed = newMovementSpeed;
    }

    public void SetJumpForce(float newJumpForce)
    {
        jumpForce = newJumpForce;
    }
}
