using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : Character
{
    [Space(5)]

    public int partyID;

    public int level;

    public float experience;
    public float maxExperience { get { return Constants.experience + (level * 15); } }

    [Space(5)]

    public float health;
    public float stamina;
    public float maxHealth { get { return Constants.health + (level * 13); } }
    public float maxStamina { get { return Constants.stamina + (level * 7); } }

    public float damage { get { return equipments[sideWeapon && sideWeapon.col.enabled ? 9 : 8].itemValue + (level * 12); } }
    public float defence { get { return (shield ? equipments[9].itemWeight * 3 : equipments[8].itemWeight * 2) + (level * 2); } }
    public float armour { get { return equipments[0].itemValue + equipments[1].itemValue + equipments[2].itemValue + equipments[3].itemValue + equipments[4].itemValue + equipments[5].itemValue + equipments[6].itemValue + equipments[7].itemValue; } }

    private float burden { get { return equipments[0].itemWeight + equipments[1].itemWeight + equipments[2].itemWeight + equipments[3].itemWeight + equipments[4].itemWeight + equipments[5].itemWeight + equipments[6].itemWeight + equipments[7].itemWeight; } }
    public float speedMultiplier { get { return ((1 + level * 0.06f) - ((1 + level * 0.06f)) * burden / 350); } }
    public int wage { get{ return Constants.wage + level * 5; } }

    protected override void Init()
    {
        base.Init();

        //BattleManager.s.parties[partyID].members.Add(this);

        health = maxHealth;
        stamina = maxStamina;
    }

    protected virtual void Tick()
    {
        if (health <= 0) { return; }

        CalculateStats();
    }

    private void CalculateStats()
    {
        level = Mathf.Clamp(level, 1, 4);
        experience = Mathf.Clamp(experience, 0, maxExperience);
        health = Mathf.Clamp(health, 0, maxHealth);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        animator.SetFloat("SpeedMultiplier", speedMultiplier);
    }

    public float HealthDamageOutput(float enemyArmour)
    {
        return damage - (damage * enemyArmour / 250);
    }

    public float StaminaDamageOutput(float enemyDefence)
    {
        return defence - (defence * enemyDefence / 100);
    }
}