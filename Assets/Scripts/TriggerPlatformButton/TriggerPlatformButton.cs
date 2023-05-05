using UnityEngine;
using UnityEngine.Events;

public class TriggerPlatformButton : MonoBehaviour
{
    [SerializeField] private string triggerWithTagName;
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private UnityEvent triggerEnterEvents;
    [SerializeField] private UnityEvent triggerExitEvents;
    
    private Material _initMaterial;
    private SpriteRenderer _spriteRenderer;
    private bool _isTriggered;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initMaterial = _spriteRenderer.material;
        _isTriggered = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_isTriggered && 
            other.collider.gameObject.CompareTag(triggerWithTagName)
            && WasHitFromAbove())
        {
            _isTriggered = true;
            _spriteRenderer.material = outlineMaterial;
            triggerEnterEvents?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (_isTriggered &&
            other.collider.gameObject.CompareTag(triggerWithTagName))
        {
            _isTriggered = false;
            _spriteRenderer.material = _initMaterial;
            triggerExitEvents?.Invoke();
        }
    }

    private bool WasHitFromAbove()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.1f);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag(triggerWithTagName))
                return true;
        }
        return false;
    }

}

