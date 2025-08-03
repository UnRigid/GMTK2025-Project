using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Pan : MonoBehaviour, IInteractable, IInventoriable
{

    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] Transform DynamicInteractParent;
    [SerializeField] GameObject[] UsableItems;// 0-Scissors
    [SerializeField] GameObject[] ObtainableItems;// 0-rust
    [SerializeField] Sprite[] Icons;// 0-Rust

    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }

    void GetRust()
    {
        InventoryManager.PickUpItem(ObtainableItems[0], Icons[0]);
                Destroy(DynamicInteractParent.GetChild(0).gameObject);

    }

    public void Interact(Vector3 mousePos)
    {
        if (InventoryManager.CheckForItem(UsableItems[0]))
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
            button.onClick.AddListener(GetRust);
        }
    }

    public Vector3 Scale()
    {
        return new Vector3(500,500,.75f);
    }
}
