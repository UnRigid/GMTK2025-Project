using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static GameObject[] HeldItems = new GameObject[4];
    static InventoryManager instance;

    private void Awake() {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }


    public void Craft()
    {
        
    }





    public static void AddItem(GameObject Item)
    {
        HeldItems[HeldItems.Length] = Item;
    }

    public static void RemoveItem(GameObject Item)
    {
        for (int i = 0; i < HeldItems.Length; i++)
        {
            if (HeldItems[i] == Item)
            {
                for (int j = 0; j < HeldItems.Length - 1 - i; j++)
                {
                    HeldItems[i] = HeldItems[i + j];

                }
                HeldItems[HeldItems.Length - 1] = null;
                break;
            }
        }
    }

    

}
