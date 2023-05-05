using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RingItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ringSprite;
    [SerializeField] private UnityEvent executeAfterRingPickedUp;
    [SerializeField] private int numberOfSwitches;
    [SerializeField] private AnimationCurve switchCurve;

    private float _delay;
    private bool _wasRingPicked;
    
    private void Start()
    {
        _wasRingPicked = false;
    }

    public void PickUpRing()
    {
        if (_wasRingPicked) return;
        _wasRingPicked = true;
        ringSprite.sprite = null;
        StartCoroutine(UniverseChaos());
    }
    
    IEnumerator UniverseChaos()
    {
        UniverseSwitchManager.Instance.SetUniverse(Universe.Red);
        for (int i = 0; i < numberOfSwitches; i++)
        {
            _delay = switchCurve.Evaluate(i);
            UniverseSwitchManager.Instance.SwitchUniverse();
            yield return new WaitForSeconds(_delay);
        }
        executeAfterRingPickedUp?.Invoke();
        Destroy(gameObject);
    }
}
