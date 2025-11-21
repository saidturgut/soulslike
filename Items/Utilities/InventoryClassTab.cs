using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryClassTab : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [HideInInspector] public Item.ItemClass clas;

    private Text text;

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();

        clas = (Item.ItemClass)System.Enum.Parse(typeof(Item.ItemClass), (name));

        if (clas == Item.ItemClass.Null)
        {
            InventoryManager.s.selectedTab = this;
        }
    }

    private void Update()
    {
        if (MainPanel.s.mainPanelDisabled) { if (name == "Null") { InventoryManager.s.selectedTab = this; } return; }

        if (InventoryManager.s.selectedTab == this)
        {
            text.color = Color.white;
        }
        else
        {
            text.color = InventoryManager.s.inventoryColors[1];
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (InventoryManager.s.selectedTab != this)
        {
            InventoryManager.s.SetUpInventory(clas);

            InventoryManager.s.selectedTab = this;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        GetComponent<Image>().color = text.color = InventoryManager.s.inventoryColors[2];

    }
    public void OnPointerExit(PointerEventData data)
    {
        GetComponent<Image>().color = text.color = Color.black;
    }
}
