using System.Collections;
using UnityEngine;

public class AIMelee : AICombat
{
    private bool decideAttack;
    private bool decidingAttack;

    private void Start() 
    { Init(); }

    private void Update()
    {
        if (health <= 0) { KillCharacter(); return; }

        if (!onBattle) { return; }

        base.Tick();

        CheckStates();
    }

    private void CheckStates()
    {
        if (!attackFlag && !decidingAttack && !decideAttack) { StartCoroutine(DecideAttack()); }

        if (!locomotionFlag || !target || (!decideAttack && !decidingAttack)) { motor.UpdateState(); return; }

        if (onPanic) { motor.UpdateState(false, 2, true); return; }

        if (!decideAttack)
        {
            if (motor.distance < Constants.strafeDistance)
            {
                //deciding attack on strafe distance

                motor.UpdateState(true, motor.randomState, false);
            }
            else
            {   //getting to strafe distance

                motor.UpdateState(false, 3, false);
            }
        }
        else
        {
            if (weapon && motor.distance > weapon.dat.attackDistance)
            {
                //decided to attack and dashing

                motor.UpdateState(false, motor.distance > Constants.strafeDistance ? 3 : 2, false);
            }
            else
            {   //attacking

                motor.UpdateState();

                if (stamina <= 1 || !weapon) { return; }

                Attack(sideWeapon && Random.Range(0, 100) > 50 ? 1 : 0);

                decideAttack = false;
            }
        }
    }

    private IEnumerator DecideAttack()
    {
        decidingAttack = true;

        float startTime = Time.time;

        while (Time.time < startTime + Random.Range(Constants.attackCooldown.x, Constants.attackCooldown.y))
        {
            yield return null;
        }

        decideAttack = true;

        //if (Random.Range(0, 100) > 85 && party.targetEnemyParty.partyMembers.Count != 1) { party.FindNewTarget(this); }

        StartCoroutine(CancelAttack());

        decidingAttack = false;
    }

    private IEnumerator CancelAttack()
    {
        float startTime = Time.time;

        while (Time.time < startTime + 1.5f)
        {
            yield return null;
        }

        if (Random.Range(0, 100) > 60)
        {
            decideAttack = false;
        }
    }
}