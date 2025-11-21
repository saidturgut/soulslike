using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : CharacterMotor
{
    [HideInInspector] public Vector3 input;
    private Vector3 inputSmooth;
    private bool sprintCooldown;
        
    private void Start()
    {
        base.Init();
    }

    private void Update()
    {
        SetAgentSpeed();
        HandleAnimations();
        MovePlayer();
    }

    protected void MovePlayer()
    {
        HandleStamina();

        SetMoveDirection();

        UpdateState();
        RotatePlayer();

        if (!stats.locomotionFlag) { return; }

        agent.Move(moveDirection * agent.speed * Time.deltaTime);
    }

    private void SetMoveDirection()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        inputSmooth = Vector3.Lerp(inputSmooth, !stats.attackFlag ? input : Vector3.zero, 6.5f * Time.deltaTime);

        var right = GameObject.FindGameObjectWithTag("MainCamera").transform.right;
        right.y = 0;
        var forward = Quaternion.AngleAxis(-90, Vector3.up) * right;

        moveDirection = (inputSmooth.x * right) + (inputSmooth.z * forward);

        moveDirection.y = 0;
    }

    private void UpdateState()
    {
        if (stats.locomotionFlag && inputSmooth.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift) && !sprintCooldown)
            {
                currentState = 3;

                return;
            }
            if (Input.GetKey(KeyCode.V))
            {
                currentState = 1;

                return;
            }

            currentState = 2;
        }
        else
        {
            currentState = 0;
        }
    }

    protected override void HandleAnimations()
    {
        var newInput = new Vector2(verticalSpeed, horizontalSpeed);

        inputMagnitude = Mathf.Clamp(currentState == 1 ? newInput.magnitude - 0.5f : (currentState == 3 ? newInput.magnitude + 0.5f : newInput.magnitude), 0, currentState == 1 ? 0.5f : currentState == 3 ? 1.5f : 1f);

        base.HandleAnimations();
    }

    protected override void HandleStamina()
    {
        if (stats.stamina <= 1) { sprintCooldown = true; }
        if (stats.stamina > 15) { sprintCooldown = false; }

        base.HandleStamina();
    }

    private void RotatePlayer()
    {
        if (cantTurn) { return; }
        float turningSpeed = (stats.attackFlag || Statics.GetAnimatorState(animator, "Deflect") || Statics.GetAnimatorState(animator, "ShieldDeflect") ? 35 : 10);

        Vector3 desiredForward = currentState != 3 ? 
            Vector3.RotateTowards(transform.forward, CameraController.s.transform.forward, turningSpeed * Time.deltaTime, .1f) :
            Vector3.RotateTowards(transform.forward, moveDirection, 15 * Time.deltaTime, .1f);

        desiredForward.y = 0;

        transform.rotation = Quaternion.LookRotation(desiredForward);
    }
}
