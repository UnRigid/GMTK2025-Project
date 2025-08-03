using UnityEngine;

public class Closet : MonoBehaviour, IInteractable
{
    public void Interact(Vector3 mousePos)
    {
        GameController.MainAnimator.SetTrigger("OpenCloset");
        Transform Overlay = transform.GetChild(0);
        
    }

    public bool InRange()
    {
        Transform Overlay = transform.GetChild(0);
        bool IsInRange = Overlay.GetComponent<MeshRenderer>().enabled;

        return IsInRange;
    }
}
