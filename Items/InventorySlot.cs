using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public bool unavailable;
    public Item item;
    private Image icon;
    private Image slotImg;

    private void Start()
    {
        icon = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        slotImg = GetComponent<Image>();

        transform.GetChild(0).GetComponent<Image>().sprite = item.itemIcon;
        transform.GetChild(1).GetComponent<Text>().text = item.itemAmount > 1 ? "x" + item.itemAmount.ToString() : "";
        transform.GetChild(2).GetComponent<Text>().text = item.itemWeight.ToString() + " KG";
        transform.GetChild(3).GetComponent<Text>().text = item.itemPrice.ToString();
    }

    private void Update()
    {
        UpdateSlot();

        if (InventoryManager.s.selectedSlot != this) { icon.color = InventoryManager.s.inventoryColors[3]; return; }

        icon.color = InventoryManager.s.inventoryColors[1];

        DropItem();
        TransferItem();
    }

    private void UpdateSlot()
    {
        unavailable = !InventoryManager.s.targetCharacter.GetComponent<Player>() && item.itemClass == Item.ItemClass.Weapon &&
            !Statics.GetUsableItemsByClass(item.itemSubClass, InventoryManager.s.targetCharacter.characterClass);
        slotImg.color = !unavailable ? Color.black : InventoryManager.s.inventoryColors[6];
    }

    private void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            InventoryManager.s.targetContainer[InventoryManager.s.contID].items.Remove(item);

            item.itemID = 0;

            Destroy(gameObject);

            //Drop Item
        }
    }

    private void TransferItem()
    {
        if (Input.GetMouseButtonDown(0) && InventoryManager.s.targetContainer[1])
        {
            int whereTo = InventoryManager.s.contID == 0 ? 1 : 0;

            ItemFunctions.AddItem(InventoryManager.s.targetContainer[whereTo], item.itemID, item.itemAmount);

            InventoryManager.s.targetContainer[InventoryManager.s.contID].items.Remove(item);

            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        InventoryManager.s.selectedSlot = this;
    }

    public void OnPointerExit(PointerEventData data)
    {
        InventoryManager.s.selectedSlot = null;
    }
}
