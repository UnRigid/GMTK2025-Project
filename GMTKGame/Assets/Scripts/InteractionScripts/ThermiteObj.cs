using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThermiteObj : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] Transform DynamicInteractParent;
    [SerializeField] GameObject[] UsableItems;// 0-lighter

    [SerializeField] GameObject Particles;
    
    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }

    void IgniteThermite()
    {
        Particles.SetActive(true);
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
            UseScissorsText.text = "Ignite Thermite";
            RectTransform rectTransform = UseScissorsButton.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(170, 30);

            Button button = UseScissorsButton.GetComponent<Button>();
            button.onClick.AddListener(IgniteThermite);
        }
    }
}
