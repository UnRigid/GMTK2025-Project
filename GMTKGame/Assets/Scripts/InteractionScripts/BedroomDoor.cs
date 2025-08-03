using UnityEngine;

public class BedroomDoor : MonoBehaviour, IInteractable
{
    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }

    public void Interact(Vector3 m)
    {
        GameController.MainAnimator.SetTrigger("OpenBDoor");
    }
}
