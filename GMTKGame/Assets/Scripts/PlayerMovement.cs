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

    [SerializeField]GameObject parent;
    Animator animator;
    
    

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

        _PlayerControls.Movement.Enable();

        animator = gameObject.GetComponent<Animator>();
        

    }

    private void FixedUpdate()
    {
        Move();
        Look();
        if (PlayerRB.linearVelocity.magnitude >= .1f)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

    }

    void Move()
    {
        Vector2 _InputRead = _PlayerControls.Movement.Walk.ReadValue<Vector2>();

        float LookAngle = Vector2.Angle(Vector2.up, _InputRead);


        if (_InputRead != Vector2.zero)
        {
            if (_InputRead.x > 0)
            {
                LookAngle += 100;
            }
            else if (_InputRead.x < 0)
            {
                LookAngle = -LookAngle;
                LookAngle += 100;
            }
            else
            {
                LookAngle += 100;
            }
            parent.transform.localEulerAngles = new Vector3(parent.transform.eulerAngles.x, LookAngle,parent.transform.eulerAngles.z);

        }
        
        


        Vector3 MoveDirection = transform.forward * _InputRead.x * -1 + transform.right * _InputRead.y;
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



        //Clamp: -30 - +40

        float _xRot = ActiveCamera.transform.rotation.eulerAngles.y;
        if (_xRot >= 330 || _xRot <= 40)
        {
            ActiveCamera.transform.RotateAround(camera.transform.position, Vector3.up, _InputRead.x * Time.fixedDeltaTime * LookSpd);

        }
        else if (_xRot < 330 && _xRot > 180)
        {
            ActiveCamera.transform.RotateAround(camera.transform.position, Vector3.up, 330 - _xRot);

        }
        else if (_xRot > 40 && _xRot < 180)
        {
            ActiveCamera.transform.RotateAround(camera.transform.position, Vector3.up, 40 - _xRot);
        }

        //Clamp 20 - 60
        float _yRot = camera.transform.rotation.eulerAngles.x;
        if (_yRot >= 20 && _yRot <= 60)
        {
            camera.transform.Rotate(Vector3.right, _InputRead.y * Time.fixedDeltaTime * -1 * LookSpd);

        }
        else if (_yRot > 60)
        {
            camera.transform.Rotate(Vector3.right, 60 - _yRot);
        }
        else if (_yRot < 20)
        {
            camera.transform.Rotate(Vector3.right, 20 - _yRot);
        }
        // Don't understand this clamp, but it works
        
        
        
    }

    private void OnDestroy()
    {

        _PlayerControls.Movement.Disable();
        
    }

}
