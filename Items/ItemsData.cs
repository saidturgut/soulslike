using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    public static ItemsData s { get; private set; }

    public List<Item> items;

    private void Awake()
    {
        s = this;

        items.Add(new Item());

        string[] lines = Resources.Load<TextAsset>("ItemsData").text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] splitData = lines[i].Split(',');
            Item item = new Item();
            item.itemName = splitData[0];
            item.itemInfo = splitData[1];
            int.TryParse(splitData[2], NumberStyles.Any, CultureInfo.InvariantCulture, out item.itemID);
            item.itemAmount = 1;
            float.TryParse(splitData[3], NumberStyles.Any, CultureInfo.InvariantCulture, out item.itemValue);
            float.TryParse(splitData[4], NumberStyles.Any, CultureInfo.InvariantCulture, out item.itemWeight);
            float.TryParse(splitData[5], NumberStyles.Any, CultureInfo.InvariantCulture, out item.itemPrice);
            item.itemClass = (Item.ItemClass)System.Enum.Parse(typeof(Item.ItemClass), (splitData[6]));
            item.itemType = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), (splitData[7]));
            item.itemSubClass = (Item.ItemSubClass)System.Enum.Parse(typeof(Item.ItemSubClass), (splitData[8]));

            items.Add(item);
        }
    }

    public Item ReturnItem(int id)
    {
        return new Item(items[id].itemName, items[id].itemInfo,
            items[id].itemID, items[id].itemAmount, items[id].itemValue,
            items[id].itemWeight, items[id].itemPrice,
            items[id].itemClass, items[id].itemType, items[id].itemSubClass);
    }
}
