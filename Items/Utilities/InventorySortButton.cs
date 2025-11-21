using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySortButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image icon;

    private void Awake()
    {
        icon = GetComponent<Image>();

        if (name == "type")
        {
            InventoryManager.s.selectedSort = this;
        }
    }

    private void Update()
    {
        if (MainPanel.s.mainPanelDisabled) { if (name == "type") { InventoryManager.s.selectedSort = this; } return; }

        if (InventoryManager.s.selectedSort == this)
        {
            icon.color = Color.white;
        }
        else
        {
            icon.color = InventoryManager.s.inventoryColors[1];
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        InventoryManager.s.selectedSort = this;

        InventoryManager.s.SetUpInventory(InventoryManager.s.selectedTab.clas);
    }

    public void SortInventory()
    {
        switch (name)
        {
            case "type":
                {
                    InventoryManager.s.targetContainer[InventoryManager.s.contID].items = InventoryManager.s.targetContainer[InventoryManager.s.contID].items.OrderBy(e => e.itemType).ToList();
                    break;
                }
            case "price":
                {
                    InventoryManager.s.targetContainer[InventoryManager.s.contID].items = InventoryManager.s.targetContainer[InventoryManager.s.contID].items.OrderByDescending(e => e.itemPrice).ToList();
                    break;
                }
            case "weight":
                {
                    InventoryManager.s.targetContainer[InventoryManager.s.contID].items = InventoryManager.s.targetContainer[InventoryManager.s.contID].items.OrderByDescending(e => e.itemWeight).ToList();
                    break;
                }
            case "quantity":
                {
                    InventoryManager.s.targetContainer[InventoryManager.s.contID].items = InventoryManager.s.targetContainer[InventoryManager.s.contID].items.OrderByDescending(e => e.itemAmount).ToList();
                    break;
                }

        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        transform.localScale = new Vector2(1.2f, 1.2f);

    }
    public void OnPointerExit(PointerEventData data)
    {
        transform.localScale = new Vector2(1, 1);
    }
}
