using UnityEngine;
using System.Threading.Tasks;

public class GameController : MonoBehaviour
{

    [SerializeField] GameObject[] Cameras = new GameObject[2];
    [SerializeField] int DelayTimeMS = 10000;
    static GameController instance;
    int Camera_Index = 0;
    bool GameHasntEnded = true;
    [SerializeField] bool EnableCameraSwitch = false;
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
        MainFunc();
    }

    async void MainFunc()
    {
        Cameras[0].SetActive(true);
        Cameras[1].SetActive(false);
        while (GameHasntEnded)
        {
            if (EnableCameraSwitch)
            {
                PlayerMovement.ActiveCamera = Cameras[Camera_Index];
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
            }
            
        }
    }

    void OnDestroy()
    {
        GameHasntEnded = false;
    }
}
