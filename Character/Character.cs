using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public CharacterMotor charMotor;

    public Statics.CharacterClass characterClass;

    public Item[] equipments = new Item[10];

    [HideInInspector] public WeaponObject mainWeapon;
    [HideInInspector] public WeaponObject sideWeapon;
    [HideInInspector] public ShieldObject shield;

    [HideInInspector] public Transform[] scabbards = new Transform[6];
    [HideInInspector] public SkinnedMeshRenderer bodyMesh;

    [HideInInspector] public bool attackFlag;
    public bool locomotionFlag { get { return animator.GetCurrentAnimatorStateInfo(3).IsName("Override"); } }

    public bool onBattle;

    public Character target;

    protected virtual void Init()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        charMotor = GetComponent<CharacterMotor>();

        bodyMesh = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();

        scabbards[0] = animator.GetBoneTransform(HumanBodyBones.Spine).GetChild(1);
        scabbards[1] = animator.GetBoneTransform(HumanBodyBones.UpperChest).GetChild(3);
        scabbards[2] = animator.GetBoneTransform(HumanBodyBones.UpperChest).GetChild(4);
        scabbards[3] = animator.GetBoneTransform(HumanBodyBones.UpperChest).GetChild(5);
        scabbards[4] = animator.GetBoneTransform(HumanBodyBones.Spine).GetChild(2);
        scabbards[5] = animator.GetBoneTransform(HumanBodyBones.Spine).GetChild(3);

        CheckEquipments();

        //onBattle = true;
    }

    public void CheckEquipments()
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            if (equipments[i].itemID != 0)
            {
                equipments[i] = ItemsData.s.ReturnItem(equipments[i].itemID);

                ItemFunctions.EquipItem(equipments[i], this);
            }
        }
    }

    protected void ResetWeapons()
    {
        if (mainWeapon) { mainWeapon.col.enabled = false; }
        if (sideWeapon) { sideWeapon.col.enabled = false; }
    }
}