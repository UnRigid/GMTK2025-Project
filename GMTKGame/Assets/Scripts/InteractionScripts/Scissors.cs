using UnityEngine;

public class Scissors : MonoBehaviour, IInteractable
{



    public void Interact()
    {
        InventoryManager.AddItem(gameObject);
        transform.parent.gameObject.SetActive(false);
    }

    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }
}
