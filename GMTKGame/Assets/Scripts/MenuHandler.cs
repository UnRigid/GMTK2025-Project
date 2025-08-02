using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject CreditsUI;
    public void Play()
    {
        SceneManager.LoadScene(1);

    }

    public void Quit()
    {
        Application.Quit();
    }


    public void Credits()
    {
        CreditsUI.SetActive(true);

    }
    public void CloseCredits()
    {
        CreditsUI.SetActive(false);
    }


}
