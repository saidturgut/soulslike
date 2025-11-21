using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public List<Item> items;

    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == "")
            {
                items[i] = ItemsData.s.ReturnItem(items[i].itemID);
            }
        }
    }
}
