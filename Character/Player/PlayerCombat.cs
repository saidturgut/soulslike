using UnityEngine;

public abstract class PlayerCombat : CharacterCombat
{
    [HideInInspector] public bool cantAction;
    private bool canCombo;
    protected PlayerMotor motor;

    protected override void Init()
    {
        base.Init();

        motor = GetComponent<PlayerMotor>();
        if (mainWeapon) { weapon = mainWeapon; }
    }

    protected override void Tick()
    {
        base.Tick();

        Checkings();

        UpdateIndicators();

        if (!MainPanel.s.mainPanelDisabled) { return; }

        HandleAttacking();

        HandleBlocking();
    }

    private void HandleAttacking()
    {
        if ((stamina <= 1 || (attackFlag || !locomotionFlag) && !canCombo) || !onBattle) { return; }

        if (Input.GetMouseButtonDown(0) && mainWeapon && mainWeapon.dat.weaponType != 2)
        {
            canCombo = false;

            Attack(0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && sideWeapon && !sideWeapon.notActive)
        {
            canCombo = false;

            Attack(1);
        }
    }

    private void HandleBlocking()
    {
        if (Input.GetMouseButtonDown(1) && onBattle && stamina > 2 && (mainWeapon || shield) && (locomotionFlag || !cantAction || Statics.GetAnimatorState(animator, "Reload") || deflectSuccessFlag) && !deflectFlag)
        {
            ResetVariables();

            Block();
        }
    }

    private void Checkings()
    {
        if (locomotionFlag) { if (!attackFlag) { ResetVariables(); } }
        if (charMotor.cantTurn) { ResetWeapons(); }
    }

    private void UpdateIndicators()
    {
        if (!UIManager.s.onBattleIndicators.activeInHierarchy) { return; }

        UIManager.s.StaminaBar.fillAmount = stamina / maxStamina;

        UIManager.s.HealthBar.fillAmount = health / maxHealth;
    }

    #region SmallFunctions

    public override void ResetVariables()
    {
        base.ResetVariables();
        cantAction = false;
        canCombo = false;
    }

    public void CheckCombo(int which)
    {
       /* if (motor.input.magnitude > 0.15f && which == 1)
        {
            canCombo = false;

            animator.CrossFade("Override", 0.25f);
        }
        else
        {
            canCombo = which == 0 ? false : true;
        }*/

        canCombo = which == 0 ? false : true;

        cantAction = false;
    }

    #endregion
}
