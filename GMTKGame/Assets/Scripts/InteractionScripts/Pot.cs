using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Pot : MonoBehaviour, IInteractable, IInventoriable
{
    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] Transform DynamicInteractParent;
    [SerializeField] GameObject[] UsableItems;// 0-Rust 1-ChoppedCan
    [SerializeField] GameObject[] ObtainableItems;// 0-Thermite
    [SerializeField] Sprite[] Icons;// 0-thermite
    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }


    void GetThermite()
    {
        InventoryManager.RemoveItem(UsableItems[0]);
        InventoryManager.RemoveItem(UsableItems[1]);
        InventoryManager.PickUpItem(ObtainableItems[0], Icons[0]);
    }

    public void Interact(Vector3 mousePos)
    {
        if (InventoryManager.CheckForItem(UsableItems[0]) && InventoryManager.CheckForItem(UsableItems[1]))
        {
            GameObject UseScissorsButton = Instantiate(ButtonPrefab, mousePos, Quaternion.identity, DynamicInteractParent);
            if (DynamicInteractParent.GetChild(0).gameObject != UseScissorsButton)
            {
                Destroy(DynamicInteractParent.GetChild(0).gameObject);
            }
            Transform UseScissorsChild = UseScissorsButton.transform.GetChild(0);
            TMP_Text UseScissorsText = UseScissorsChild.GetComponent<TMP_Text>();
            UseScissorsText.text = "Craft Thermite with Chopped Can and Rust";
            RectTransform rectTransform = UseScissorsButton.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(280, 75);

            Button button = UseScissorsButton.GetComponent<Button>();
            button.onClick.AddListener(GetThermite);
        }
    }


    public Vector3 Scale()
    {
        return new Vector3(619, 107, .6f);
    }
}
