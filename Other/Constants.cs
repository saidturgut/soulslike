using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    [Header("BasicStats")]

    public const float health = 200;
    public const float stamina = 95;
    public const float experience = 120;
    public const int wage = 17;

    [Header("Locomotion")]

    public const float walkingSpeed = 2;
    public const float runningSpeed = 6.5f;
    public const float sprintSpeed = 8;

    [Header("AI Stats")]

    public const float strafeDistance = 7;
    public const float viewDistance = 45;
    public const float chaseDistance = 10;
    public const float shootingDistance = 20;
    public const float retreatDistance = 9;
    public const float panicReduceRate = 35;
    public const float panicBorder = 200;

    public static Vector2 attackCooldown = new Vector2(2, 3);

    [Space(10)]

    public const float attackRange = 2;
    public const float blockRange = 4;
    public const float droppedBehindMultiplier = 2.83f;
    public const float stayOnPointTime = 2;

    [Header("Party Stats")]
    public const float partyStaminaRegenRate = 15;
    public const float partySprintStaminaCon = 20;
    public const float tempFatigueDebuff = 0.2f;

    [Header("Other")]

    public const float staminaRegenRate = 30;
    public const float sprintStaminaCon = 10;
    public const int characterLootChance = 50;
    public const float deflectAngle = 90;
    public const float deflectChance = 75;
    public const float boltForce = 4000;
}