using UnityEngine;

public class Lighter : MonoBehaviour, IInteractable, IInventoriable
{

    [SerializeField]Sprite LighterImage;

    public int LighterIndex;
    public void Interact(Vector3 m)
    {
        LighterIndex = InventoryManager.PickUpItem(gameObject, LighterImage);
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
        return new Vector3(1738,973,.45f);
    }


}
