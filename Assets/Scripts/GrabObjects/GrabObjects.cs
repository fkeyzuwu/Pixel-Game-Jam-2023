using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerController))]
public class GrabObjects : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform grabRayPoint;
    [SerializeField] private Transform grabParent;
    [SerializeField] private float rayDistance;

    private GameObject _grabbedObject;
    private Transform _grabbedObjectParent;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = PlayerController.Instance;
    }

    private void Update()
    {

        if (Input.GetButtonDown("Grab"))
        {
            if (_grabbedObject == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(
                    grabRayPoint.position,
                    grabRayPoint.transform.right,
                    rayDistance,
                    GameLayersManager.Instance.grabbableObjectsLayerMask);
        
                if (hit.collider == null) return;

                _grabbedObject = hit.collider.gameObject;
                _grabbedObjectParent = hit.transform.parent;
                _grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                _grabbedObject.transform.position = grabPoint.position;
                _grabbedObject.transform.SetParent(grabParent);
            }
            else
            {
                _grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                _grabbedObject.transform.SetParent(_grabbedObjectParent);
                _grabbedObject = null;  
            }
        }
    }
}
