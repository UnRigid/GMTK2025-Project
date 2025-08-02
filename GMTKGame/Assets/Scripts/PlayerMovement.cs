using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody PlayerRB;
    [HideInInspector]public static GameObject ActiveCamera;
    PlayerControls _PlayerControls;
    [Header("Movement")]
    [SerializeField] float WalkSpd;
    static PlayerMovement instance;
    [SerializeField] float LookSpd = 5f;
    
    

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
        PlayerRB = gameObject.GetComponent<Rigidbody>();
        
        _PlayerControls = new PlayerControls();

        _PlayerControls.Enable();
        

        

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
        Vector3 _Move = new Vector3(MoveDirection.x, 0, MoveDirection.z);
        PlayerRB.linearVelocity = _Move * WalkSpd * 20 * Time.fixedDeltaTime + new Vector3(0, PlayerRB.linearVelocity.y, 0);
    }



    // bool isOnGround()
    // {
    //     Ray ray = new Ray(transform.position, Vector3.down);
    //     if (Physics.Raycast(ray, out RaycastHit hit, 1.1f))
    //     {
    //         if (hit.transform.gameObject.tag == "Ground")
    //         {
    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    void Look()
    {
        Vector2 _InputRead = _PlayerControls.Movement.Look.ReadValue<Vector2>();
        Camera camera = ActiveCamera.GetComponentInChildren<Camera>();


        //Lateral Rotation
        // ActiveCamera.transform.Rotate(Vector3.up, _InputRead.x * Time.fixedDeltaTime * LookSpd);
        ActiveCamera.transform.RotateAround(camera.transform.position, Vector3.up, _InputRead.x * Time.fixedDeltaTime * LookSpd);

        //Horizontal Rotation
        camera.transform.Rotate(Vector3.right, _InputRead.y * Time.fixedDeltaTime * -1 * LookSpd);
        //Clamp: -30 - +40

        float _xRot = ActiveCamera.transform.rotation.eulerAngles.y;
        if (_xRot < -30)
        {
            _xRot = -30;
        }
        else if (_xRot > 40)
        {
            _xRot = 40;
        }
        ActiveCamera.transform.localEulerAngles = new Vector3(ActiveCamera.transform.rotation.x, _xRot, ActiveCamera.transform.rotation.z);
        

        //Clamp vertical
        // float _yRot = ActiveCamera.transform.rotation.eulerAngles.x;

        // if(_yRot < 360 - 80f && _yRot > 270){
        //     _yRot = 360 - 80f;
        // }
        // else if(_yRot > 80f && _yRot < 90){
        //     _yRot = 80f;
        // }
        //Debug.Log(_yRot + "  " + PlayerViewCam.transform.rotation.eulerAngles.x );
        // ActiveCamera.transform.localEulerAngles = new  Vector3(_yRot, 0,0);
    }

    private void OnDestroy()
    {

        _PlayerControls.Disable();
        
    }

}
