using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleFunctions
{
    private static List<Character> targetless;

    public static void ShootBolt(Rigidbody bolt, Vector3 direction)
    {
        bolt.transform.GetChild(0).gameObject.SetActive(true);
        bolt.transform.SetParent(null);
        bolt.GetComponent<BoxCollider>().enabled = true;
        bolt.useGravity = true;
        bolt.constraints = RigidbodyConstraints.None;
        bolt.AddForce(direction * Constants.boltForce);
        bolt.GetComponent<DestroyOnTime>().enabled = true;
    }

    public static void SelectFirstTargets(Party party)
    {
        targetless = new List<Character>();

        for (int i = 0; i < party.targetEnemyParty.members.Count; i++)
        {
            party.members[i].target = party.targetEnemyParty.members[i];
            party.targetEnemyParty.members[i].target = party.members[i];
        }

        foreach (Character stats in party.members)
        {
            if (!stats.target)
            {
                targetless.Add(stats);
            }
        }

        if (targetless.Count < party.targetEnemyParty.members.Count)
        {
            for (int i = 0; i < targetless.Count; i++)
            {
                targetless[i].target = party.targetEnemyParty.members[i];

                targetless.Remove(targetless[i]);
            }
        }
        else
        {
            while (targetless.Count == 0)
            {
                for (int i = 0; i < targetless.Count; i++)
                {
                    targetless[i].target = party.targetEnemyParty.members[i];

                    targetless.Remove(targetless[i]);
                }
            }
        }
    }

}
