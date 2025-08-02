using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionControl : MonoBehaviour
{

    [SerializeField] float InteractRange;
    [SerializeField] float Ydelta;
    Vector3 Origin;
    PlayerControls _playercontrols;

    InteractionControl instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        _playercontrols = new PlayerControls();
        _playercontrols.Interactions.Enable();

        _playercontrols.Interactions.LMB.performed += TryInteract;
    }

    private void Update()
    {
        CheckInteractable();
    }


    void TryInteract(InputAction.CallbackContext callbackContext)
    {
        Vector3 MousePosition = _playercontrols.Interactions.MousePos.ReadValue<Vector2>();
        Ray ray = PlayerMovement.ActiveCamera.transform.GetChild(0).GetComponent<Camera>().ScreenPointToRay(new Vector3(MousePosition.x, MousePosition.y));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, 1 << 6))
        {
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                if (interactable.InRange())
                {
                interactable.Interact();
                    
                }
            }
        }
    }




    private void CheckInteractable()
    {
        Origin = transform.position;
        Origin.y += Ydelta;
        Collider[] Results = Physics.OverlapSphere(Origin, InteractRange, 1 << 6);
        Collider[] All = Physics.OverlapSphere(Vector3.zero, Mathf.Infinity, 1 << 6);
        foreach (Collider collider in Results)
        {

            if (collider.transform.GetChild(0).TryGetComponent(out OverlayTag overlayTag))
            {
                Transform Overlay = overlayTag.transform;

                Overlay.GetComponent<MeshRenderer>().enabled = true;
            }

        }

        var AllL = All;
        var Res = Results;
        var invalid = All.Except<Collider>(Res);

        foreach (Collider collider in invalid)
        {
            if (collider.transform.GetChild(0).TryGetComponent(out OverlayTag overlayTag))
            {
                Transform Overlay = overlayTag.transform;

                Overlay.GetComponent<MeshRenderer>().enabled = false;
            }
        }


    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Origin, InteractRange);
    }

    private void OnDestroy() {
        _playercontrols.Interactions.LMB.performed -= TryInteract;
        _playercontrols.Interactions.Disable();
    }

}
