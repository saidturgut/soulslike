using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMotor : CharacterMotor
{
    [HideInInspector] public int randomState;
    public float distance;

    [HideInInspector] public bool enableStrafe;
    private bool retreating;

    private AICombat com;

    private void Start()
    {
        base.Init();

        com = GetComponent<AICombat>();

        moveDirection = transform.right;

        randomState = Random.Range(1, 3);
        StartCoroutine(ChangeStrafeDirection());

        UpdateState();
    }

    private void Update()
    {
        base.HandleStamina();

        SetAgentSpeed();

        RunCurrentState();
        HandleAnimations();
    }

    public void UpdateState(bool strafe, int state, bool retreat)
    {
        enableStrafe = strafe;

        currentState = state;

        if (retreat) { if (!retreating) { StartCoroutine(Retreat()); } return; }
        else { retreating = false; }

        agent.SetDestination(com.target.transform.position);
    }

    public void UpdateState() { enableStrafe = false; currentState = 0; }

    private void RunCurrentState()
    {
        if (!com.target) { return; }

        distance = Vector3.Distance(com.target.transform.position, transform.position);

        agent.isStopped = true;

        if (currentState != 0)
        {
            agent.Move(moveDirection * agent.speed * Time.deltaTime);

            if (enableStrafe)
            {
                FaceToTarget(com.target.transform.position);
            }
            else
            {
                moveDirection = transform.forward;

                FaceToTarget(agent.steeringTarget);
            }
        }
        else
        {
            if (!cantTurn)
            {
                FaceToTarget(com.target.transform.position);
            }
        }

        if (!stats.locomotionFlag) { return; }

        if (currentState == 3) { stats.stamina -= Time.deltaTime * Constants.sprintStaminaCon; }
        else { stats.stamina += Time.deltaTime * Constants.staminaRegenRate; }
    }

    protected override void HandleAnimations()
    {
        inputMagnitude = ((float)currentState) / 2;

        base.HandleAnimations();
    }

    public void FaceToTarget(Vector3 target)
    {
        Vector3 lookPos = target - transform.position;
        lookPos.y = 0;

        if (lookPos == Vector3.zero) { return; }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), 0.1f);
    }

    private IEnumerator ChangeStrafeDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f + Random.Range(3, 5));

            if (enableStrafe)
            {
                moveDirection = Random.Range(0, 2) == 1 ? transform.right : -transform.right;

                randomState = Random.Range(1, 3);
            }
        }
    }

    private IEnumerator Retreat()
    {
        retreating = true;

        agent.SetDestination(BattleManager.s.retreatPoints.transform.GetChild(Random.Range(0, 4)).transform.position);

        float startTime = Time.time;

        while (Time.time < startTime + 3)
        {
            yield return null;
        }

        retreating = false;
    }
}