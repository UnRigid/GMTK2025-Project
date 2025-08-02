using UnityEngine;

public interface IInteractable
{
    void Interact();

    bool InRange();//Check if object is in interact range
}
