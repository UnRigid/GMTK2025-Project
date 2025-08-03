using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bed : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] Transform DynamicInteractParent;
    [SerializeField] GameObject[] UsableItems;// 0-Scissors
    [SerializeField] GameObject[] ObtainableItems;// 0-Rope
    [SerializeField] Sprite[] Icons;// 0-Rope

    public int RopeIndex;
    
    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }

    void CutBedding()
    {
        if (InventoryManager.CheckForItem(UsableItems[0]))
        {
            RopeIndex = InventoryManager.PickUpItem(ObtainableItems[0], Icons[0]);

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
            button.onClick.AddListener(CutBedding);

        
    }
}
