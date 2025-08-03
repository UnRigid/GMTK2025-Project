using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{

    [SerializeField] static Sprite EmptySprite;
    public static List<GameObject> HeldItems = new List<GameObject>();
    static InventoryManager instance;

    static GameObject[] ItemHolders = new GameObject[4];


    static GameObject[] TempItemHolders = new GameObject[4];

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
        TempItemHolders = GameObject.FindGameObjectsWithTag("ItemHolders");
        foreach (GameObject holder in TempItemHolders)
        {
            ItemHolders[int.Parse(holder.name.Remove(0,4))-1] = holder;
            

            holder.SetActive(false);
        }

        foreach (GameObject item in ItemHolders)
        {
            Debug.Log(item.name);
        }
    }

    





    public static int PickUpItem(GameObject Item, Sprite sprite)
    {

        Refresh();
        if (HeldItems.Count < 4)
        {
            HeldItems.Add(Item);
        }
        Debug.Log("Picked up " + Item.name + ", Index is " + HeldItems.IndexOf(Item));
        Vector3 Dimesions = Item.GetComponent<IInventoriable>().Scale();
        int Index = HeldItems.IndexOf(Item);

        ItemHolders[Index].SetActive(true);
        Image DisplayedItem = ItemHolders[Index].GetComponent<Image>();

        DisplayedItem.sprite = sprite;

        RectTransform rectTransform = ItemHolders[Index].GetComponent<RectTransform>();



        rectTransform.sizeDelta = new Vector2(Dimesions.x, Dimesions.y);
        rectTransform.localScale = new Vector3(Dimesions.z, Dimesions.z, Dimesions.z);
        Refresh();

        return Index;

    }

    public static void RemoveItems(GameObject[] Items)
    {
        Refresh();
        foreach (GameObject Item in Items)
        {
            // Refresh();
            int Index = HeldItems.IndexOf(Item);
            Debug.Log("removed " + Item.name + ", Index " + Index);

            ItemHolders[Index].GetComponent<Image>().sprite = EmptySprite;
            ItemHolders[Index].SetActive(false);
            // Refresh();


        }
        foreach (GameObject Item in Items)
        {
            // Refresh();
            HeldItems.Remove(Item);
            // Refresh();
        }
        Refresh();

    }

    static void Refresh()
    {
        List<Sprite> sprites = new List<Sprite>();
        foreach (GameObject itemHolder in ItemHolders)
        {
            if (itemHolder.activeSelf)
            {
                sprites.Add(itemHolder.GetComponent<Image>().sprite);


            }
        }

        List<GameObject> Temp = new List<GameObject>();


        foreach (Sprite sprite in sprites)
        {
            string name = sprite.name;
            GameObject itemToadd = null;
            foreach (GameObject item in HeldItems)
            {
                if (name == item.name)
                {
                    itemToadd = item;
                }
            }
            Temp.Add(itemToadd);
        }

        HeldItems = Temp;
        string log = "Refreshed ";
        foreach (Sprite sprite in sprites)
        {
            log += sprite.name;
            log += sprites.IndexOf(sprite);
            log += " ";
        }

        foreach (GameObject _gameObject in HeldItems)
        {
            log += _gameObject.name;
            log += HeldItems.IndexOf(_gameObject);
            log += " ";
        }
        Debug.Log(log);
        // List<Sprite> sprites = new List<Sprite>;

        // foreach (GameObject item in HeldItems)
        // {
        //     sprites.Add(item.GetComponent<>)
        // }

        // foreach (GameObject item in HeldItems)
        // {
        //     ItemHolders[HeldItems.IndexOf(item)].GetComponent<Image>().sprite = sprites[HeldItems.IndexOf(item)];
        // }
    }

    public static bool CheckForItem(GameObject Item)
    {
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
