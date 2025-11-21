using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObject : MonoBehaviour
{
    private Animator animator;
    private Character owner;

    public bool notActive;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        owner = GetComponentInParent<Character>();
    }

    private void Update()
    {
        notActive = (owner.mainWeapon && owner.mainWeapon.dat.weaponType != 0) || !owner.onBattle;

        if (notActive)
        {
            Transform targetParent = animator.GetBoneTransform(HumanBodyBones.UpperChest).GetChild(6);

            if (transform.parent != targetParent) { transform.SetParent(targetParent); }
        }
        else
        {
            Transform targetParent = animator.GetBoneTransform(HumanBodyBones.LeftHand);

            if (transform.parent != targetParent) { transform.SetParent(targetParent); }
        }

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
