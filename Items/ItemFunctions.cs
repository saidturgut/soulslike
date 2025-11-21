using System.Collections;
using UnityEngine;

public static class ItemFunctions
{
    public static Transform armours = Resources.Load<GameObject>("ArmourMeshes").transform;
    public static Transform emptyChar = Resources.Load<GameObject>("EmptyCharacter").transform;
    public static Material emptyMaterial = Resources.Load<Material>("EmptyMaterial");

    public static void AddItem(ItemContainer targetCont, int id, int amount)
    {
        targetCont.items.Add(ItemsData.s.ReturnItem(id));

        targetCont.items[targetCont.items.Count - 1].itemAmount = amount;
    }

    public static void EquipItem(Item item, Character character)
    {
        character.equipments[(int)item.itemType - 1] = ItemsData.s.ReturnItem(item.itemID);

        if (item.itemClass == Item.ItemClass.Weapon) { AssignWeapons(item, character); }
        else
        {
            SkinnedMeshRenderer eqRend = character.transform.GetChild(1).transform.GetChild((int)item.itemType - 1).GetComponent<SkinnedMeshRenderer>();

            eqRend.sharedMesh = armours.transform.GetChild(item.itemID - 1).GetComponent<SkinnedMeshRenderer>().sharedMesh;

            eqRend.materials = armours.transform.GetChild(item.itemID - 1).GetComponent<SkinnedMeshRenderer>().sharedMaterials;
        }

        if (MainPanel.s.mainPanelDisabled || character != InventoryManager.s.targetCharacter) { return; }

        InventoryManager.s.targetContainer[InventoryManager.s.contID].items.Remove(item);

        item.itemID = 0;

        Object.Destroy(InventoryManager.s.selectedSlot.gameObject);
    }

    private static void AssignWeapons(Item item, Character character)
    {
        GameObject eqModel = Object.Instantiate(Resources.Load<GameObject>("Weapons/" + item.itemID.ToString()), 
            character.GetComponent<Animator>().GetBoneTransform(item.itemType == Item.ItemType.RightHand ? HumanBodyBones.RightHand : HumanBodyBones.LeftHand));
        eqModel.name = item.itemID.ToString();

        switch (item.itemSubClass)
        {
            case Item.ItemSubClass.Dagger:
                {
                    character.sideWeapon = eqModel.GetComponent<WeaponObject>();
                    character.shield = null;

                    break;
                }
            case Item.ItemSubClass.Shield:
                {
                    character.shield = eqModel.GetComponent<ShieldObject>();
                    character.sideWeapon = null;

                    break;
                }
            default :
                {
                    character.mainWeapon = eqModel.GetComponent<WeaponObject>();

                    break;
                }
        }
    }

    public static void DeEquipItem(int id)
    {
        if (InventoryManager.s.targetCharacter.equipments[id].itemClass == Item.ItemClass.Weapon)
        {
            switch (InventoryManager.s.targetCharacter.equipments[id].itemSubClass)
            {
                case Item.ItemSubClass.Dagger:
                    {
                        Object.Destroy(InventoryManager.s.targetCharacter.sideWeapon.gameObject);

                        break;
                    }
                case Item.ItemSubClass.Shield:
                    {
                        Object.Destroy(InventoryManager.s.targetCharacter.shield.gameObject);

                        break;
                    }
                default:
                    {
                        Object.Destroy(InventoryManager.s.targetCharacter.mainWeapon.gameObject);

                        break;
                    }
            }
        }
        else
        {
            SkinnedMeshRenderer eqRend = InventoryManager.s.targetCharacter.transform.GetChild(1).transform.GetChild(id).GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer emptyEqRend = emptyChar.GetChild(1).transform.GetChild(id).GetComponent<SkinnedMeshRenderer>();

            eqRend.sharedMesh = emptyEqRend.sharedMesh;
            eqRend.materials = emptyEqRend.sharedMaterials;
        }

        InventoryManager.s.AddSlot(id,
            InventoryManager.s.targetCharacter.equipments[id].itemClass == InventoryManager.s.selectedTab.clas || InventoryManager.s.selectedTab.clas == Item.ItemClass.Null);

        InventoryManager.s.targetCharacter.equipments[id] = new Item();

        //if (id == 2) { InventoryManager.s.targetCharacter.GetComponent<Animator>().SetFloat("WeaponType", 0); }
    }
}
