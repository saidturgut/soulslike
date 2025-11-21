using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image icon;
    private Image border;

    [HideInInspector] public int id;

    private void Start()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
        border = transform.GetChild(2).GetComponent<Image>();

        id = transform.GetSiblingIndex();
    }

    private void Update()
    {
        UpdateIcon();
        EquipSlot();
        DeEquipSlot();
        UpdateLeftHandSlot();
    }

    private void EquipSlot()
    {
        if (InventoryManager.s.selectedSlot &&
    (InventoryManager.s.selectedSlot.item.itemClass == Item.ItemClass.Weapon || InventoryManager.s.selectedSlot.item.itemClass == Item.ItemClass.Armour) &&
    (int)InventoryManager.s.selectedSlot.item.itemType - 1 == id)
        {
            if (InventoryManager.s.selectedSlot.unavailable || 
                (InventoryManager.s.targetCharacter.GetComponent<Player>() && 
                !Statics.GetUsableItemsByClass(InventoryManager.s.selectedSlot.item.itemSubClass, Statics.CharacterClass.Player))) { 
                border.color = InventoryManager.s.inventoryColors[5]; return; }

            border.color = InventoryManager.s.inventoryColors[4];

            if (Input.GetMouseButtonDown(1))
            {
                if (InventoryManager.s.targetCharacter.equipments[id].itemID != 0)
                {
                    ItemFunctions.DeEquipItem(id);
                }

                ItemFunctions.EquipItem(InventoryManager.s.selectedSlot.item, InventoryManager.s.targetCharacter);
            }
        }
        else if (!InventoryManager.s.selectedEqSlot)
        {
            border.color = InventoryManager.s.inventoryColors[3];
        }
    }

    private void DeEquipSlot()
    {
        if (InventoryManager.s.selectedEqSlot == this)
        {
            border.color = InventoryManager.s.inventoryColors[1];

            if (Input.GetMouseButtonDown(1) && InventoryManager.s.targetCharacter.equipments[id].itemID != 0)
            {
                ItemFunctions.DeEquipItem(id);
            }
        }
        else if (!InventoryManager.s.selectedSlot)
        {
            border.color = InventoryManager.s.inventoryColors[3];
        }
    }

    private void UpdateLeftHandSlot()
    {
        if (id != 9) { return; }

        if ((InventoryManager.s.targetCharacter.sideWeapon && InventoryManager.s.targetCharacter.sideWeapon.notActive) || 
            (InventoryManager.s.targetCharacter.shield && InventoryManager.s.targetCharacter.shield.notActive && InventoryManager.s.targetCharacter.mainWeapon && InventoryManager.s.targetCharacter.mainWeapon.dat.weaponType != 0)) {
            ItemFunctions.DeEquipItem(id); }
    }

    private void UpdateIcon()
    {
        if (id == 9 && InventoryManager.s.targetCharacter.mainWeapon && InventoryManager.s.targetCharacter.mainWeapon.dat.weaponType == 1) 
        {
            icon.enabled = true;
            icon.color = InventoryManager.s.inventoryColors[7];
            icon.sprite = InventoryManager.s.targetCharacter.equipments[8].itemIcon;

            return; 
        }

        icon.enabled = InventoryManager.s.targetCharacter.equipments[id].itemID != 0;
        icon.color = Color.white;
        if (InventoryManager.s.targetCharacter.equipments[id].itemID != 0)
        {
            icon.sprite = InventoryManager.s.targetCharacter.equipments[id].itemIcon;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        InventoryManager.s.selectedEqSlot = this;
    }

    public void OnPointerExit(PointerEventData data)
    {
        InventoryManager.s.selectedEqSlot = null;
    }
}