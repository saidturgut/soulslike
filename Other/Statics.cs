using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics 
{
    public enum CharacterClass
    {
        Player,
        Swordsman,
        Axeman,
        Knight,
        Spearman,
        Halberdier,
        Crossbowman,
    }

    public static bool GetUsableItemsByClass(Item.ItemSubClass wepClass,CharacterClass charClass)
    {
        switch (charClass)
        {
            case CharacterClass.Player:
                {
                    return !((Player.s.mainWeapon && Player.s.mainWeapon.dat.weaponType == 1) && (wepClass == Item.ItemSubClass.Shield || wepClass == Item.ItemSubClass.Dagger));
                }
            case CharacterClass.Swordsman:
                {
                    return wepClass == Item.ItemSubClass.Sword || wepClass == Item.ItemSubClass.Shield || wepClass == Item.ItemSubClass.Dagger;
                }
            case CharacterClass.Axeman:
                {
                    return wepClass == Item.ItemSubClass.Axe || wepClass == Item.ItemSubClass.Shield || wepClass == Item.ItemSubClass.Dagger;
                }
            case CharacterClass.Knight:
                {
                    return wepClass == Item.ItemSubClass.Longsword;
                }
            case CharacterClass.Halberdier:
                {
                    return wepClass == Item.ItemSubClass.Polearm;
                }
            case CharacterClass.Spearman:
                {
                    return wepClass == Item.ItemSubClass.Spear || wepClass == Item.ItemSubClass.Shield || wepClass == Item.ItemSubClass.Dagger;
                }
            case CharacterClass.Crossbowman:
                {
                    return wepClass == Item.ItemSubClass.Crossbow || wepClass == Item.ItemSubClass.Dagger;
                }
        }

        return false;
    }

    public static bool GetAnimatorState(Animator animator, string name)
    {
        return animator.GetCurrentAnimatorStateInfo(3).IsName(name);
    }

    public static bool NotOnDagger(Animator animator)
    {
        return !GetAnimatorState(animator, "Dagger0") && !GetAnimatorState(animator, "Dagger1") && !GetAnimatorState(animator, "Dagger2");
    }
}
