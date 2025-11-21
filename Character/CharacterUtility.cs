using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterUtility : CharacterStats
{
    private float leftHandIKWeight;
    [HideInInspector] public Vector3 lastAttackCode;

    public bool deflectFlag { get { return Statics.GetAnimatorState(animator, "Deflect") || Statics.GetAnimatorState(animator, "ShieldDeflect"); } }
    public bool deflectSuccessFlag { get { return Statics.GetAnimatorState(animator, "DeflectSuccess") || Statics.GetAnimatorState(animator, "ShieldDeflectSuccess"); } }

    protected override void Init() { base.Init(); }

    protected override void Tick() 
    { 
        base.Tick();

        if (Input.GetKeyDown(KeyCode.B) && locomotionFlag) { onBattle = !onBattle; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DamageCollider>() && other.GetComponent<DamageCollider>().com.attackCode != lastAttackCode)
        {
            DamageCollider dam = other.GetComponent<DamageCollider>();

            if(dam.com.partyID == partyID) { return; }

            lastAttackCode = dam.com.attackCode;

            if (GetComponent<AIMelee>()) { GetComponent<AIMelee>().ResetVariables(); }
            if (GetComponent<AIRanged>()) { GetComponent<AIRanged>().ResetVariables(); }
            if (GetComponent<PlayerCombat>()) { GetComponent<PlayerCombat>().ResetVariables(); }

            DecideReaction(dam);

            if (!dam.transform.parent) { Destroy(dam.gameObject); }

            if ((dam.com.GetComponent<Player>() && dam.transform.parent) || GetComponent<Player>()) { StartCoroutine(CameraShaker.s.ShakeCamera(0.15f, 0.08f)); }
        }
    }

    private void DecideReaction(DamageCollider dam)
    {
        if ((deflectFlag || deflectSuccessFlag) && Vector3.Angle(transform.forward, dam.com.transform.forward) > Constants.deflectAngle)
        {
            stamina -= dam.com.StaminaDamageOutput(defence);
            dam.com.stamina -= StaminaDamageOutput(dam.com.defence);

            animator.CrossFade(shield && !shield.notActive ? "ShieldDeflectSuccess" : "DeflectSuccess", 0.1f);

            Instantiate(BattleManager.s.sparks, dam.GetComponent<BoxCollider>().ClosestPoint(transform.position), transform.rotation);

            SoundManager.s.Play(Random.Range(0, 10), transform, !shield ? SoundManager.SoundType.Metal : SoundManager.SoundType.Shield);
        }
        else
        {
            health -= dam.com.HealthDamageOutput(armour);

            animator.CrossFade("TakeHit" + Random.Range(1, 5), 0);

            Instantiate(BattleManager.s.blood, dam.GetComponent<BoxCollider>().ClosestPoint(transform.position), transform.rotation, transform);

            SoundManager.s.Play(dam.transform.parent ? Random.Range(0, 10) : Random.Range(10, 16), transform, SoundManager.SoundType.VividImpact);
            SoundManager.s.Play(Random.Range(0, 10), transform, SoundManager.SoundType.EnemyBasic);

            if (GetComponent<AICombat>()) { GetComponent<AICombat>().panicMeter += dam.com.HealthDamageOutput(armour); }
        }
    }

    public void KillCharacter()
    {
        BattleManager.s.parties[partyID].members.Remove(this);

        SoundManager.s.Play(Random.Range(10, 15), transform, SoundManager.SoundType.EnemyBasic);

        if (!GetComponent<Player>()) { Destroy(transform.GetChild(2).transform.GetChild(1).gameObject); }

        foreach (var comp in gameObject.GetComponents<Component>())
        {
            if (!(comp is Transform) && !(comp is ItemContainer))
            {
                Destroy(comp);
            }
        }

        if (mainWeapon) { Destroy(mainWeapon); }
        if (sideWeapon) { Destroy(sideWeapon); }
        if (shield) { Destroy(GetComponentInChildren<ShieldObject>()); }

        transform.SetParent(GameManager.s.transform);

        gameObject.AddComponent<MakeRagdoll>();
    }

    public void OnAnimatorIK()
    {
        if (mainWeapon && mainWeapon.dat.weaponType != 0 && !Statics.GetAnimatorState(animator, "Reload") && Statics.NotOnDagger(animator) && onBattle)
        {
            leftHandIKWeight = Mathf.Lerp(leftHandIKWeight, 1, 2 * Time.deltaTime);

            animator.SetIKPosition(AvatarIKGoal.LeftHand, mainWeapon.transform.GetChild(0).transform.GetChild(0).position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, mainWeapon.transform.GetChild(0).transform.GetChild(0).rotation);
            animator.SetIKHintPosition(AvatarIKHint.LeftElbow, transform.GetChild(2).transform.GetChild(0).position);
        }
        else
        {
            leftHandIKWeight = Mathf.Lerp(leftHandIKWeight, 0, 2 * Time.deltaTime);
        }

        animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, leftHandIKWeight);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandIKWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandIKWeight);
    }
}
