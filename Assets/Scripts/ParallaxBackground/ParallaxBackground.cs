using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    [SerializeField] private float parallaxEffect;

    private Camera _cam;
    private float _length;
    private float _startPos;

    void Start()
    {
        _cam = Camera.main;
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        Vector3 camTransformPosition = _cam.transform.position;
        float xMovementBounds = camTransformPosition.x * (1 - parallaxEffect); // How far we have moved relative to the camera
        float xMovement = camTransformPosition.x * parallaxEffect;
        transform.position = new Vector3(_startPos + xMovement, transform.position.y, transform.position.z);
        if (xMovementBounds > _startPos + _length)
            _startPos += _length;
        else if (xMovementBounds < _startPos - _length)
            _startPos -= _length;
    }
}