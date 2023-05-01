using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(Rigidbody2D), 
    typeof(Collider2D))
]
public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.0f;

    [Header("Jump")] 
    [SerializeField] private float jumpForce = 16.0f;
    
    private Rigidbody2D _rb;
    private float _horizontalInput;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

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
        _rb.velocity = new Vector2(_horizontalInput * movementSpeed, _rb.velocity.y);
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private bool CanJump()
    {
        return IsGrounded();
    }

    private bool IsGrounded()
    {
        Bounds boxColliderBounds = _boxCollider.bounds;
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
