using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Can : MonoBehaviour, IInteractable, IInventoriable
{

    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] Transform DynamicInteractParent;
    [SerializeField] GameObject[] UsableItems;// 0-Scissors
    [SerializeField] GameObject[] ObtainableItems;// 0-Chopped Can
    [SerializeField] Sprite[] Icons;// 0-Chopped Can

    public int ChoppedCanIndex;

    void CutCan()
    {
        if (InventoryManager.CheckForItem(UsableItems[0]))
        {
ChoppedCanIndex = InventoryManager.PickUpItem(ObtainableItems[0], Icons[0]);
        
        Destroy(gameObject.transform.parent.gameObject);
        }
        Destroy(DynamicInteractParent.GetChild(0).gameObject);

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
            UseScissorsText.text = "Use Scissors";
            RectTransform rectTransform = UseScissorsButton.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(180, 30);

            Button button = UseScissorsButton.GetComponent<Button>();
            button.onClick.AddListener(CutCan);
        
    }

    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }

    public Vector3 Scale()
    {
        return new Vector3(666,375,1.2f);
    }

}
