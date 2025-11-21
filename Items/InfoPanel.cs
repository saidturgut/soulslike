using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (MainPanel.s.mainPanelDisabled) { return; }

        rect.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (InventoryManager.s.selectedSlot || (InventoryManager.s.selectedEqSlot && InventoryManager.s.targetCharacter.equipments[InventoryManager.s.selectedEqSlot.id].itemID != 0))
        {
            transform.localScale = new Vector2(1.3f, 1.3f);

            Item itm = ItemsData.s.ReturnItem(InventoryManager.s.selectedSlot ? InventoryManager.s.selectedSlot.item.itemID : InventoryManager.s.targetCharacter.equipments[InventoryManager.s.selectedEqSlot.id].itemID);

            transform.GetChild(0).GetComponent<Text>().text = itm.itemName;
            transform.GetChild(1).GetComponent<Text>().text = itm.itemSubClass.ToString();

            transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(InventoryManager.s.targetContainer[1]);
            transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text != "");
            transform.GetChild(3).transform.GetChild(2).GetComponentInChildren<Text>().text = itm.itemClass == Item.ItemClass.Consumable ? "Use" : "Equip";

            transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text =
                itm.itemClass == Item.ItemClass.Weapon ? "Damage : " + itm.itemValue :
                itm.itemClass == Item.ItemClass.Armour ? "Armour : " + itm.itemValue :
                itm.itemClass == Item.ItemClass.Consumable ? "Energy : " + itm.itemValue : "";
        }
        else
        {
            transform.localScale = Vector2.zero;
        }
    }
}
