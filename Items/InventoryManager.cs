using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager s { get; private set; }

    public ItemContainer[] targetContainer;
    [HideInInspector] public int contID;

    public CharacterStats targetCharacter;

    public int whichCharacter;

    public List<EquipmentSlot> equipmentSlots;

    public InventorySlot selectedSlot;
    public EquipmentSlot selectedEqSlot;

    public InventoryClassTab selectedTab;
    public InventorySortButton selectedSort;

    [SerializeField] private GameObject slotPrefab;

    [HideInInspector] public Transform slotContent;

    public Color[] inventoryColors;

    [SerializeField] private Text weightText;

    [SerializeField] private float totalWeight;

    [SerializeField] private float maxWeight;

    private void Awake()
    {
        s = this;

        targetContainer = new ItemContainer[2];
        targetContainer[0] = Player.s.GetComponent<ItemContainer>();

        targetCharacter = Player.s.GetComponent<CharacterStats>();

        slotContent = GetComponentInChildren<ContentSizeFitter>().transform;

        DestroyInventory();
    }

    private void Update()
    {
        targetCharacter = Player.s.GetComponent<CharacterStats>();

        totalWeight = 0;

        foreach (Item itm in targetContainer[contID].items)
        {
            totalWeight += itm.itemWeight;
        }

        weightText.text = totalWeight + " / " + maxWeight + " KG";
    }

    public void SetUpInventory(Item.ItemClass clas)
    {
        if (slotContent.childCount != 0)
        {
            DestroyInventory();
        }

        selectedSort.SortInventory();

        foreach (Item itm in targetContainer[contID].items)
        {
            if (itm.itemClass == clas || clas == Item.ItemClass.Null)
            {
                Instantiate(slotPrefab, slotContent).GetComponent<InventorySlot>().item = itm;
            }
        }
    }

    public void AddSlot(int eqId, bool create)
    {
        ItemFunctions.AddItem(targetContainer[contID], targetCharacter.equipments[eqId].itemID, 1);

        if (!create) { return; }

        Instantiate(slotPrefab, slotContent).GetComponent<InventorySlot>().item = targetContainer[contID].items[targetContainer[contID].items.Count - 1];
    }

    public void DestroyInventory()
    {
        foreach (Transform child in slotContent)
        {
            Destroy(child.gameObject);
        }
    }
}
