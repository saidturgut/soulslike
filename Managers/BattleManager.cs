using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager s { get; private set; }

    public Party[] parties;

    public GameObject blood;
    public GameObject sparks;
    public GameObject bolt;
    public Transform retreatPoints;
    public Transform emptyEquipments;

    private void Awake() { s = this; BattleFunctions.SelectFirstTargets(parties[1]); }
}