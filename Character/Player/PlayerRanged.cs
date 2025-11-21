using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerRanged : PlayerCombat
{
    private float lookIK;
    private bool pressingButton;
    private bool crossbowLoaded;
    [HideInInspector] public bool reloading;

    private WeaponObject crossbow;
    private Rigidbody bolt;
    private SkinnedMeshRenderer crossbowMesh;

    protected override void Init()
    {
        base.Init();
        crossbowLoaded = true;
    }

    protected override void Tick()
    {
        base.Tick();
        HandleCrossbow();
    }

    private void HandleCrossbow()
    {
        if ((mainWeapon && mainWeapon.dat.weaponType != 2) || !mainWeapon || !onBattle) { return; }

        crossbow = mainWeapon;
        if (!crossbowMesh) { crossbowMesh = crossbow.GetComponentInChildren<SkinnedMeshRenderer>(); }

        crossbowMesh.SetBlendShapeWeight(0, crossbowLoaded ? 100 : 0);
        if (crossbowLoaded && !bolt) { bolt = Instantiate(BattleManager.s.bolt, crossbowMesh.transform.GetChild(1)).GetComponent<Rigidbody>(); }

        if (reloading) { return; }

        if (Input.GetMouseButtonDown(0) && !crossbowLoaded && !attackFlag)
        {
            reloading = true;
            animator.CrossFade("Reload", 0.15f);
        }

        if (Input.GetMouseButton(0) && !attackFlag && !pressingButton && crossbowLoaded)
        {
            charMotor.cantTurn = false;

            pressingButton = true;

            attackFlag = true;

            animator.CrossFade("Aim", 0.15f);

            SoundManager.s.Play(Random.Range(6, 8), transform, SoundManager.SoundType.Ranged);
        }

        if (Input.GetMouseButtonUp(0))
        {
            pressingButton = false;

            if (Statics.GetAnimatorState(animator, "Aim") && attackFlag)
            {
                Invoke("Shoot", 0.1f);
            }
        }
    }

    private void Shoot()
    {
        attackCode = transform.localPosition + transform.localEulerAngles + transform.forward;

        weapon = mainWeapon;

        BattleFunctions.ShootBolt(bolt, transform.forward);

        crossbowLoaded = false;

        animator.SetTrigger("Shoot");

        SoundManager.s.Play(Random.Range(0, 6), transform, SoundManager.SoundType.Ranged);
    }

    private void ResetCrossbow()
    {
        crossbowLoaded = true;
        reloading = false;
        pressingButton = false;
    }

    public void Reloaded()
    {
        crossbowLoaded = true;
        reloading = false;
    }

    /*private void OnAnimatorIK(int layerIndex)
    {
        lookIK = Mathf.Lerp(lookIK, man.attackFlag ? 0.4f : 0, 2 * Time.deltaTime);

        animator.SetLookAtWeight(lookIK, 0.5f, 1, 0, 0.5f);

        Vector3 rayDir = Camera.main.transform.forward;

        Ray lookAtRay = new Ray(transform.position, rayDir);

        animator.SetLookAtPosition(lookAtRay.GetPoint(25));
    }*/
}
