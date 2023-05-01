using UnityEngine;

[RequireComponent(
    typeof(Rigidbody2D), 
    typeof(BoxCollider2D))
]
public class CharacterBasicController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.0f;

    [Header("Jump")] 
    [SerializeField] private float jumpForce = 16.0f;

    protected Rigidbody2D Rigidbody;
    protected BoxCollider2D BoxCollider;
    
    public Vector2 MoveDelta { get; private set; }
    public bool IsWalking { get; private set; }
    
    public virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
    }
    
    protected void Move(Vector2 moveDelta)
    {
        IsWalking = moveDelta != Vector2.zero;
        MoveDelta = moveDelta;
        Rigidbody.velocity = new Vector2(moveDelta.x * movementSpeed, Rigidbody.velocity.y);
    }
    
    protected void Jump()
    {
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, jumpForce);
    }
}
