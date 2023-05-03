using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class GrabObjects : MonoBehaviour
{
    #region Singleton
    public static GrabObjects Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform grabRayPoint;
    [SerializeField] private Transform grabParent;
    [SerializeField] private float rayDistance;

    private GrabbableObject _grabbedObject;
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
                if (hit.collider.GetComponent<GrabbableObject>() == null) return;
                
                _grabbedObject = hit.collider.GetComponent<GrabbableObject>();
                _grabbedObjectParent = hit.transform.parent;
                _grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                _grabbedObject.transform.position = grabPoint.position;
                _grabbedObject.transform.SetParent(grabParent);
                UpdatePlayerForces();
            }
            else
            {
                _grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                _grabbedObject.transform.SetParent(_grabbedObjectParent);
                _grabbedObject = null;
                _grabbedObjectParent = null;
                UpdatePlayerForces();
            }
        }
    }

    private void UpdatePlayerForces()
    {
        if (_grabbedObject != null)
        {
            _playerController.SetMovementSpeed(_grabbedObject.playerMovementSpeed);
            _playerController.SetJumpForce(_grabbedObject.playerJumpForce);
        }
        else
        {
            _playerController.RestoreMovementSpeedToDefault();
            _playerController.RestoreJumpForceToDefault();
        }
    }
}
