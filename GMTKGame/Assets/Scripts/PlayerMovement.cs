using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody PlayerRB;
    GameObject MainCamera;
    PlayerControls _PlayerControls;
    [Header("Movement")]
    [SerializeField] float WalkSpd;
    [SerializeField] float RunSpd;
    [SerializeField] float JumpStrength;

    private void Awake()
    {

    }

    private void Start()
    {
        PlayerRB = gameObject.GetComponent<Rigidbody>();
        MainCamera = gameObject.GetComponentInChildren<Camera>().GameObject();
        _PlayerControls = new PlayerControls();

        _PlayerControls.Enable();
        _PlayerControls.Movement.Jump.performed += Jump;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    void Move()
    {
        Vector2 _InputRead = _PlayerControls.Movement.Walk.ReadValue<Vector2>();
        Vector3 MoveDirection = transform.forward * _InputRead.y + transform.right * _InputRead.x;
        Vector3 _Move = new Vector3(MoveDirection.x, 0, MoveDirection.z) * (1 + RunSpd * _PlayerControls.Movement.Sprint.ReadValue<float>());

        PlayerRB.linearVelocity = _Move * WalkSpd * 20 * Time.fixedDeltaTime + new Vector3(0, PlayerRB.linearVelocity.y, 0);
    }

    void Jump(InputAction.CallbackContext callbackContext)
    {
        if (isOnGround())
        {
            PlayerRB.AddForce(Vector3.up * JumpStrength, ForceMode.VelocityChange);
            
        }
    }

    bool isOnGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.1f))
        {
            if (hit.transform.gameObject.tag == "Ground")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void Look()
    {
        Vector2 _InputRead = _PlayerControls.Movement.Look.ReadValue<Vector2>();
        float _xDelta = _InputRead.x * Settings.Sensitivity * .007f * Time.fixedDeltaTime * 18;
        float _yDelta = _InputRead.y * Settings.Sensitivity * .007f * Time.fixedDeltaTime * 18  ;
        
        //Lateral Rotation
        this.transform.Rotate(Vector3.up, _xDelta);

        //Horizontal Rotation
        MainCamera.transform.Rotate(Vector3.right, -_yDelta);

        //Clamp vertical
        float _yRot = MainCamera.transform.rotation.eulerAngles.x;
        
        if(_yRot < 360 - 80f && _yRot > 270){
            _yRot = 360 - 80f;
        }
        else if(_yRot > 80f && _yRot < 90){
            _yRot = 80f;
        }
        //Debug.Log(_yRot + "  " + PlayerViewCam.transform.rotation.eulerAngles.x );
        MainCamera.transform.localEulerAngles = new  Vector3(_yRot, 0,0);
    }

    private void OnDestroy()
    {
        _PlayerControls.Movement.Jump.performed -= Jump;
        _PlayerControls.Disable();
        
    }

}
