using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CharacterMotor : MonoBehaviour
{
    public int currentState;
    protected float verticalSpeed;
    protected float horizontalSpeed;
    protected float inputMagnitude;

    protected Vector3 moveDirection;

    [HideInInspector] public bool cantTurn;

    protected NavMeshAgent agent;
    protected Animator animator;
    protected CharacterStats stats;

    protected virtual void Init()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<CharacterStats>();
    }

    protected void SetAgentSpeed()
    {
        float targetSpeed = (
            currentState == 0 ? 0 : (
            currentState == 1 ? Constants.walkingSpeed : (
            currentState == 2 ? Constants.runningSpeed : Constants.sprintSpeed))) 
            * stats.speedMultiplier;

        agent.speed = Mathf.Lerp(agent.speed, targetSpeed, 7 * Time.deltaTime);
    }

    protected virtual void HandleAnimations()
    {
        verticalSpeed = transform.InverseTransformDirection(moveDirection).z;
        horizontalSpeed = transform.InverseTransformDirection(moveDirection).x;

        animator.SetFloat("InputHorizontal", horizontalSpeed, 0.15f, Time.deltaTime);
        animator.SetFloat("InputVertical", verticalSpeed, 0.15f, Time.deltaTime);
        animator.SetFloat("InputMagnitude", inputMagnitude, 0.15f, Time.deltaTime);
    }

    protected virtual void HandleStamina()
    {
        if (!stats.locomotionFlag) { return; }

        if (currentState == 3) { stats.stamina -= Time.deltaTime * Constants.sprintStaminaCon; }
        else { stats.stamina += Time.deltaTime * Constants.staminaRegenRate; }
    }

    public void Step()
    {
        if (stats.attackFlag || agent.velocity.sqrMagnitude > 0.1f) { return; }

        SoundManager.s.Play(Random.Range(0, 10), transform, SoundManager.SoundType.FootStep);
    }
}