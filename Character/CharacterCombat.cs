using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterCombat : CharacterUtility
{
    [Space(5)]

    public WeaponObject weapon;

    [HideInInspector] public int whichAttack;
    [HideInInspector] public Vector3 attackCode;

    protected void Attack(int type)
    {
        if (type == 0) { if (weapon == sideWeapon) { whichAttack = 0; } weapon = mainWeapon; }
        if (type == 1) { if (weapon == mainWeapon) { whichAttack = 0; } weapon = sideWeapon; }

        attackFlag = true;

        attackCode = transform.localPosition + transform.localEulerAngles + transform.forward;
        animator.CrossFade(weapon.dat.weaponClass + whichAttack, 0.25f);

        if (whichAttack < 2) { whichAttack++; }
        else { whichAttack = 0; }

        charMotor.cantTurn = true;
    }

    protected void Block() 
    {
        SoundManager.s.Play(Random.Range(6, 11), transform, SoundManager.SoundType.Weapon);

        animator.CrossFade(shield && !shield.notActive ? "ShieldDeflect" : "Deflect", 0.1f);
    }

    public void StartingAttack()
    {
        stamina -= weapon.dat.staminaConsumption;

        SoundManager.s.Play(Random.Range(6, 11), transform, SoundManager.SoundType.Weapon);

        charMotor.cantTurn = false;

        weapon.col.enabled = true;
    }

    public void HalfOfAttack()
    {
        charMotor.cantTurn = true;
        ResetWeapons();
    }

    public virtual void ResetVariables()
    {
        whichAttack = 0;
        charMotor.cantTurn = false;
        ResetWeapons();
    }

    public void ComboAI()
    {
    }
}
