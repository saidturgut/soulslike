using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class AICombat : CharacterCombat
{
    [HideInInspector] public bool reactedAlready;

    protected bool onPanic;
    [HideInInspector] public float panicMeter;

    protected Transform healthBar;

    protected AIMotor motor;

    protected override void Init()
    {
        base.Init();

        motor = GetComponent<AIMotor>();
        healthBar = transform.GetChild(2).transform.GetChild(1);
        if (mainWeapon) { weapon = mainWeapon; }
    }

    protected override void Tick()
    {
        base.Tick();

        Checkings();

        if (locomotionFlag) { if (!attackFlag) { ResetVariables(); } }

        HandleTarget();
        HandleBlocking();
        HandlePanic();
        HealthBar();
    }

    protected void HandleBlocking()
    {
        if (stamina > 2 && (!attackFlag || motor.cantTurn) && 
            (weapon || shield) && 
            target && 
            ((target.mainWeapon && target.mainWeapon.col.enabled) || 
            (target.sideWeapon && target.sideWeapon.col.enabled)) && 
            !reactedAlready && motor.distance <= Constants.blockRange)
        {
            reactedAlready = true;

            if (Random.Range(0, 100) > Constants.deflectChance) { return; }

            ResetVariables();

            Block();

            transform.LookAt(target.transform);

            motor.currentState = 0;
        }
    }

    private void Checkings()
    {
        if (locomotionFlag) { motor.cantTurn = false; ResetWeapons(); }
    }

    protected void HandlePanic()
    {
        panicMeter -= Constants.panicReduceRate * Time.deltaTime;
        panicMeter = Mathf.Clamp(panicMeter, 0, 500);
        onPanic = panicMeter > 200;
    }

    protected void HandleTarget()
    {
        if (target && Statics.GetAnimatorState(target.GetComponent<Animator>(), "Override")) { reactedAlready = false; }

        if (!target) { BattleManager.s.parties[partyID].FindNewTarget(this); }
    }

    protected void HealthBar()
    {
        healthBar.gameObject.SetActive(true);

        if (health > 10) { healthBar.transform.GetChild(1).GetComponent<Image>().fillAmount = health / maxHealth; }

        healthBar.transform.GetChild(1).GetComponent<Image>().color = UIManager.s.uiColors[1];

        healthBar.LookAt(healthBar.position + GameObject.FindGameObjectWithTag("MainCamera").transform.rotation * Vector3.forward, GameObject.FindGameObjectWithTag("MainCamera").transform.rotation * Vector3.up);
    }

    public void CheckCombo(int which)
    {
        if (which == 1) { Invoke("DoCombo", Random.Range(0.1f, 0.25f)); }
    }

    private void DoCombo()
    {
        ResetWeapons();

        if (Random.Range(0, 100) < 90 && motor.distance < Constants.attackRange + weapon.dat.attackDistance && stamina > 1 && target)
        {
            if (whichAttack == 3)
            {
                whichAttack = 0;
            }

            Attack(2);
        }
    }
}