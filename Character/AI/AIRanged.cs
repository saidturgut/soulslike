using System.Collections;
using UnityEngine;

public class AIRanged : AICombat
{
    private bool crossbowLoaded;
    [HideInInspector] public bool reloading;

    private Rigidbody bolt;
    private SkinnedMeshRenderer crossbowMesh;

    private void Start()
    { base.Init(); crossbowLoaded = true; }

    private void Update()
    {
        if (health <= 0) { KillCharacter(); return; }

        base.Tick();

        CheckStates();
        UpdateCrossbow();
    }

    private void CheckStates()
    {
        if (motor.distance < Constants.retreatDistance / 3 && reloading) { animator.CrossFade("Override", 0.15f); }

        if (!locomotionFlag || attackFlag || !target || reloading) { motor.UpdateState(); return; }

        if (onPanic) { motor.UpdateState(false, 3, true); return; }

        if(motor.distance > Constants.retreatDistance)
        {
            StopCoroutine(DecideOnDanger());

            if (motor.distance > Constants.shootingDistance)
            {
                motor.UpdateState(false, 2, false);
            }
            else
            {
                motor.UpdateState();

                if (crossbowLoaded)
                {
                    Aim();
                }
                else
                {
                    reloading = true;

                    animator.CrossFade("Reload", 0.15f);
                }
            }
        }
        else
        {
            if (crossbowLoaded)
            {
                motor.UpdateState();

                Aim();
            }
            else if(motor.distance < weapon.dat.attackDistance && stamina > 1)
            {
                motor.UpdateState();

                Attack(1);
            }
            else
            {
                motor.UpdateState(false, 2, true);
            }
        }
    }

    private IEnumerator DecideOnDanger()
    {
        float startTime = Time.time;

        while (Time.time < startTime + Random.Range(Constants.attackCooldown.x, Constants.attackCooldown.y))
        {
            yield return null;
        }

        if (Random.Range(0, 100) > 60)
        {
            motor.UpdateState(false, 2, true);
        }
        else
        {
            Attack(1);
        }
    }

    private void UpdateCrossbow()
    {
        if (!crossbowMesh) { if (weapon) { crossbowMesh = weapon.GetComponentInChildren<SkinnedMeshRenderer>(); } else { return; } }

        crossbowMesh.SetBlendShapeWeight(0, crossbowLoaded ? 100 : 0);
        if (crossbowLoaded && !bolt) { bolt = Instantiate(BattleManager.s.bolt, crossbowMesh.transform.GetChild(1)).GetComponent<Rigidbody>(); }
    }

    private void Aim()
    {
        if (attackFlag || !bolt) { return; }

        weapon = mainWeapon;

        attackFlag = true;

        animator.CrossFade("Aim", 0.15f);

        SoundManager.s.Play(Random.Range(6, 8), transform, SoundManager.SoundType.Ranged);

        Invoke("Shoot", Random.Range(0.6f, 1.4f));

    }

    private void Shoot()
    {
        if (!bolt) { return; }

        BattleFunctions.ShootBolt(bolt, transform.forward);
        bolt = null;

        crossbowLoaded = false;

        animator.SetTrigger("Shoot");

        SoundManager.s.Play(Random.Range(0, 6), transform, SoundManager.SoundType.Ranged);

        //if (target) { party.FindNewTarget(this); }
    }

    public void Reloaded()
    {
        crossbowLoaded = true;
        reloading = false;
    }
}