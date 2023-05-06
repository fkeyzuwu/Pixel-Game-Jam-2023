using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimator : CharacterBasicAnimator
{
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

    private PlayerController _playerController;

    public override void Start()
    {
        base.Start();
        _playerController = GetComponent<PlayerController>();
    }

    public override void Update()
    {
        base.Update();
        _animator.SetBool(IsGrounded, _playerController.IsGrounded);
    }
}
