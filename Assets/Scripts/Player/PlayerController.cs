using UnityEngine;

[RequireComponent(
    typeof(Rigidbody2D), 
    typeof(BoxCollider2D))
]
public class PlayerController : CharacterBasicController
{
    private float _horizontalInput;

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButtonDown("Jump") && CanJump())
        {
            Jump();
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
            GameLayersManager.Instance.groundLayerMask);
        return raycastHit.collider != null;
    }

}
