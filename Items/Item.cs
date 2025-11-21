using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName, itemInfo;

    public int itemID, itemAmount;

    public float itemValue;

    public float itemWeight;

    public float itemPrice;

    public ItemClass itemClass;

    public ItemType itemType;

    public ItemSubClass itemSubClass;

    public Sprite itemIcon;

    public Item(string name, string info, int id, int amount, float value, float weight, float price, ItemClass clas, ItemType type, ItemSubClass sub)
    {
        itemName = name;
        itemInfo = info;
        itemID = id;
        itemAmount = amount;
        itemValue = value;
        itemWeight = weight;
        itemPrice = price;
        itemClass = clas;
        itemType = type;
        itemSubClass = sub;

        itemIcon = Resources.Load<Sprite>("ItemIcons/" + id.ToString());
    }

    public Item()
    {

    }

    public enum ItemClass
    {
        Null,
        Weapon,
        Armour,
        Consumable,
        KeyItem,
        Other,
    }
    public enum ItemType
    {
        Null,
        Headwear,
        Coif,
        Outerwear,
        Bodywear,
        ArmPlate,
        Handwear,
        LegPlate,
        Footwear,
        RightHand,
        LeftHand,
    }

    public enum ItemSubClass
    {
        Null,
        Headwear,
        Coif,
        Outerwear,
        Bodywear,
        ArmPlate,
        Handwear,
        LegPlate,
        Footwear,
        Sword,
        Axe,
        Longsword,
        Spear,
        Polearm,
        Crossbow,
        Dagger,
        Shield,
    }
}