using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ThermiteObj : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] Transform DynamicInteractParent;
    [SerializeField] GameObject[] UsableItems;// 0-lighter

    [SerializeField] GameObject Particles;
    [SerializeField] GameObject EndGameUI;
    
    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }

    async void IgniteThermite()
    {
        if (InventoryManager.CheckForItem(UsableItems[0]))
        {
            Particles.SetActive(true);
        InventoryManager.RemoveItems(UsableItems);
        
        await Task.Delay(2000);
        EndGameUI.SetActive(true);

        GameController.MainAnimator.SetTrigger("EndGame");
        await Task.Yield();

        }Destroy(DynamicInteractParent.GetChild(0).gameObject);
    }

    public void Interact(Vector3 mousePos)
    {
        GameObject UseScissorsButton = Instantiate(ButtonPrefab, mousePos, Quaternion.identity, DynamicInteractParent);
            if (DynamicInteractParent.GetChild(0).gameObject != UseScissorsButton)
            {
                Destroy(DynamicInteractParent.GetChild(0).gameObject);
            }
            Transform UseScissorsChild = UseScissorsButton.transform.GetChild(0);
            TMP_Text UseScissorsText = UseScissorsChild.GetComponent<TMP_Text>();
            UseScissorsText.text = "Ignite Thermite";
            RectTransform rectTransform = UseScissorsButton.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(170, 30);

            Button button = UseScissorsButton.GetComponent<Button>();
            button.onClick.AddListener(IgniteThermite);
        
    }
}
