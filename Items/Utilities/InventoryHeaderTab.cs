using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryHeaderTab : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Text text;

    private int headerID;

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();
        headerID = transform.GetSiblingIndex();
    }

    private void Update()
    {
        if (MainPanel.s.mainPanelDisabled) { return; }

        if (InventoryManager.s.contID == headerID)
        {
            text.color = Color.white;
        }
        else
        {
            text.color = InventoryManager.s.inventoryColors[1];
        }

        if (headerID == 1)
        {
            if (!InventoryManager.s.targetContainer[1]) { text.text = ""; }
            else { text.text = "Loot"; }
        }
    }
    public void OnPointerClick(PointerEventData data)
    {
        if (InventoryManager.s.contID != headerID && InventoryManager.s.targetContainer[headerID])
        {
            InventoryManager.s.contID = headerID;

            InventoryManager.s.SetUpInventory(InventoryManager.s.selectedTab.clas);
        }
    }


    public void OnPointerEnter(PointerEventData data)
    {
        if (!InventoryManager.s.targetContainer[headerID]) { return; }

        GetComponent<Image>().color = 
            text.color = InventoryManager.s.inventoryColors[2];
    }
    public void OnPointerExit(PointerEventData data)
    {
        GetComponent<Image>().color = text.color = Color.black;
    }
}
