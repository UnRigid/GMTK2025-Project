using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ElectricalBox : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] Transform DynamicInteractParent;
    [SerializeField] GameObject[] UsableItems;// 0-Thermite 1-Rope
    [SerializeField] GameObject ThermiteObj;

    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }

    void PlaceThermite()
    {
        InventoryManager.RemoveItem(UsableItems[0]);
        InventoryManager.RemoveItem(UsableItems[1]);
        ThermiteObj.SetActive(true);
        gameObject.SetActive(false);

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
            UseScissorsText.text = "Place Thermite";
            RectTransform rectTransform = UseScissorsButton.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(170, 30); //Ignite Thermite: 170, 30

            Button button = UseScissorsButton.GetComponent<Button>();
            button.onClick.AddListener(PlaceThermite);
        }
    }

}
