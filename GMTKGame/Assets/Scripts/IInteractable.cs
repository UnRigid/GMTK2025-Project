using UnityEngine;

public interface IInteractable
{
    void Interact(Vector3 mousePos);

    bool InRange();//Check if object is in interact range
}
