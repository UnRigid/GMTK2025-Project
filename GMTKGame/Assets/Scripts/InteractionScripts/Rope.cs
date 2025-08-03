using UnityEngine;

public class Rope : MonoBehaviour, IInventoriable
{
    public Vector3 Scale()
    {
        return new Vector3(512, 512, .75f);
    }
}
