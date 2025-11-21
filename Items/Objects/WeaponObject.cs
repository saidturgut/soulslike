using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    [HideInInspector] public BoxCollider col;

    public int weaponClassID;

    public Weapon dat;

    [HideInInspector] public bool notActive;

    private Character owner;

    private void Awake()
    {
        owner = GetComponentInParent<Character>();

        col = GetComponentInChildren<BoxCollider>();
        col.enabled = false;

        dat = WeaponsData.s.weapons[weaponClassID];
    }

    private void Update()
    {
        if (!GetComponentInParent<Character>() || dat.staminaConsumption == 0) { return; }

        Checking();

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private void Checking()
    {
        notActive = (dat.scabbardType == 5 && owner.mainWeapon && owner.mainWeapon.dat.weaponType == 1) || !owner.onBattle;

        if (notActive)
        {
            Transform targetParent = owner.scabbards[dat.scabbardType];

            if (transform.parent != targetParent) { transform.SetParent(targetParent); }

            if (dat.scabbardType == 5) { return; }

            owner.animator.SetFloat("WeaponType", 0, 0.2f, Time.deltaTime);
            col.enabled = false;
        }
        else
        {
            if (dat.scabbardType == 5)
            {
                Transform targetParentt = owner.animator.GetBoneTransform(HumanBodyBones.LeftHand);

                if (transform.parent != targetParentt) { transform.SetParent(targetParentt); }
                return; 
            }

            Transform targetParent = owner.animator.GetBoneTransform(HumanBodyBones.RightHand);

            if (transform.parent != targetParent) { transform.SetParent(targetParent); }

            owner.animator.SetFloat("WeaponType", dat.weaponType, 0.2f, Time.deltaTime);
            owner.animator.SetFloat("WeaponSpeed", dat.weaponSpeed);
        }
    }
}
