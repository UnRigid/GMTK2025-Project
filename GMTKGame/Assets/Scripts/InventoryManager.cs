using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


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

    private void Start()
    {
        ItemHolders = GameObject.FindGameObjectsWithTag("ItemHolders");
        foreach (GameObject holder in ItemHolders)
        {
            holder.SetActive(false);
        }
    }

    public void Craft()
    {

    }





    public static void PickUpItem(GameObject Item, Sprite sprite)
    {

        if (HeldItems.Count < 4)
        {
            HeldItems.Add(Item);
        }
        Vector3 Dimesions = Item.GetComponent<IInventoriable>().Scale();
        int Index = HeldItems.IndexOf(Item);

        ItemHolders[Index].SetActive(true);
        Image DisplayedItem = ItemHolders[Index].GetComponent<Image>();

        DisplayedItem.sprite = sprite;

        RectTransform rectTransform = ItemHolders[Index].GetComponent<RectTransform>();



        rectTransform.sizeDelta = new Vector2(Dimesions.x, Dimesions.y);
        rectTransform.localScale = new Vector3(Dimesions.z, Dimesions.z, Dimesions.z);



    }

    public static void RemoveItem(GameObject Item)
    {
        int Index = HeldItems.IndexOf(Item);
        ItemHolders[Index].SetActive(false);

        HeldItems.Remove(Item);
    }

    public static bool CheckForItem(GameObject Item) {
        if (HeldItems.IndexOf(Item) != -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

}
