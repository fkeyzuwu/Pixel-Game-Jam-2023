using UnityEngine;

[RequireComponent(
    typeof(CharacterBasicController),
    typeof(Animator))
]
public class CharacterBasicAnimator : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    private CharacterBasicController _characterController;
    private Animator _animator;

    public virtual void Start()
    {
        _characterController = GetComponent<CharacterBasicController>();
        _animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        _animator.SetFloat(Horizontal, _characterController.MoveDelta.x);
        _animator.SetBool(IsWalking, _characterController.IsWalking);
    }
}
