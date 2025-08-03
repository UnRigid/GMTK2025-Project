using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.UI;
using Microsoft.Unity.VisualStudio.Editor;

public class InventoryManager : MonoBehaviour
{

    public static List<GameObject> HeldItems = new List<GameObject>();
    static InventoryManager instance;

    static GameObject[] ItemHolders = new GameObject[4];

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    private void Start() {
        ItemHolders = GameObject.FindGameObjectsWithTag("ItemHolders");
    }

    public void Craft()
    {

    }





    public static void PickUpItem(GameObject Item)
    {
        if (HeldItems.Count < 4)
        {
            HeldItems.Add(Item);
        }
        Vector3 Dimesions = Item.GetComponent<IInventoriable>().Scale();
        int Index = HeldItems.IndexOf(Item);

        Image DisplayedItem = ItemHolders[Index].GetComponent<Image>();

        

    }

    public static void RemoveItem(GameObject Item)
    {
        HeldItems.Remove(Item);
    }

    

}
