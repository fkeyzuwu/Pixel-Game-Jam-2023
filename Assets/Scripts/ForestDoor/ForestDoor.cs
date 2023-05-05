using UnityEngine;

public class ForestDoor : MonoBehaviour
{
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    [SerializeField] private int numOfLocks;
    [SerializeField] private Universe doorUniverse;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private int _numOfLocksUnlocked;
    private bool _isDoorLocked;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _numOfLocksUnlocked = 0;
        _isDoorLocked = true;
        UniverseSwitchManager.Instance.OnUniverseChangedCallback += ToggleDoorVisibility;
        ToggleDoorVisibility(UniverseSwitchManager.Instance.currentUniverse);
    }

    public void UnLockDoor()
    {
        if (_isDoorLocked)
            _numOfLocksUnlocked++;
        if (_numOfLocksUnlocked == numOfLocks)
            OpenDoor();
    }

    public void LockDoor()
    {
        if (!_isDoorLocked)
        {
            CloseDoor();
        }
        _numOfLocksUnlocked--;
    }
    
    private void OpenDoor()
    {
        _isDoorLocked = false;
        _animator.SetBool(IsOpen, true);
    }

    private void CloseDoor()
    {
        _isDoorLocked = true;
        _animator.SetBool(IsOpen, false);
    }
    
    private void ToggleDoorVisibility(Universe universe)
    {
        if (doorUniverse == Universe.None) return;

        if(doorUniverse != universe)
        {
            ToggleDoorVisibility(false);
        }
        else
        {
            ToggleDoorVisibility(true);
        }
    }

    private void ToggleDoorVisibility(bool visible)
    {
        _boxCollider2D.enabled = visible;
        _spriteRenderer.enabled = visible;
    }
}
