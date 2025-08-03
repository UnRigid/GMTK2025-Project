using UnityEngine;

public class Scissors : MonoBehaviour, IInteractable, IInventoriable
{

    [SerializeField] Sprite ScissorsImage;

    public void Interact(Vector3 m)
    {
        InventoryManager.PickUpItem(gameObject, ScissorsImage);
        transform.parent.gameObject.SetActive(false);
    }

    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }

    public Vector3 Scale()
    {
        return new Vector3(1738, 973, 0.4f);
    }


}
