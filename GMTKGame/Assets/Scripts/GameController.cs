using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    


    [SerializeField] GameObject[] Cameras = new GameObject[2];
    float[] Angles = {37.4f, 37.4f };
    [SerializeField] int DelayTimeMS = 10000;
    static GameController instance;
    int Camera_Index = 0;
    bool GameHasntEnded = true;
    [SerializeField] bool EnableCameraSwitch = false;
    GameObject playerOBJ;

    public static Animator MainAnimator;
    public static AudioSource MainAudioSource;

    PlayerControls _playerControls;
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
        GameHasntEnded = true;
        playerOBJ = GameObject.FindGameObjectWithTag("Player");
        MainAnimator = GameObject.FindGameObjectWithTag("MainAnimatorHolder").GetComponent<Animator>();
        MainAudioSource = playerOBJ.GetComponent<AudioSource>();

        MainFunc();

        _playerControls = new PlayerControls();
        _playerControls.Debug.Enable();
        _playerControls.Debug.NextCam.performed += SwitchCamera;
    }

    void SwitchCamera(InputAction.CallbackContext callbackContext)
    {
        PlayerMovement.ActiveCamera = Cameras[Camera_Index];
        playerOBJ.transform.localEulerAngles = new Vector3(0, Angles[Camera_Index], 0);
        foreach (GameObject ACam in Cameras)
        {
            if (ACam == PlayerMovement.ActiveCamera)
            {
                ACam.SetActive(true);
            }
            else
            {
                ACam.SetActive(false);
            }


        }

        
        Camera_Index += 1;
        if (Camera_Index > Cameras.Length - 1)
        {
            Camera_Index = 0;
        }
    }

    async void MainFunc()
    {
        Cameras[0].SetActive(true);
        Cameras[1].SetActive(false);
        do
        {

            PlayerMovement.ActiveCamera = Cameras[Camera_Index];
            playerOBJ.transform.localEulerAngles = new Vector3(0, Angles[Camera_Index], 0);
            foreach (GameObject ACam in Cameras)
            {
                if (ACam == PlayerMovement.ActiveCamera)
                {
                    ACam.SetActive(true);
                }
                else
                {
                    ACam.SetActive(false);
                }
            }
            Camera_Index += 1;
            if (Camera_Index > Cameras.Length - 1)
            {
                Camera_Index = 0;
            }
            await Task.Delay(DelayTimeMS);


        } while (GameHasntEnded && EnableCameraSwitch);
        await Task.Yield();
    }

    void OnDestroy()
    {
        GameHasntEnded = false;
        _playerControls.Debug.Disable();
    }
}
