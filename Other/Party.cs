using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party : MonoBehaviour
{
    public List<Character> members;

    public Party targetEnemyParty;

    private List<CharacterStats> sortingList;

    public void FindNewTarget(Character stats)
    {
        sortingList = new List<CharacterStats>();

        foreach (CharacterStats enem in targetEnemyParty.members)
        {
            sortingList.Add(enem);
        }

        sortingList = sortingList.OrderBy(e => Vector3.Distance(e.transform.position, stats.transform.position)).ToList();

        if (sortingList.Count != 0) { stats.target = sortingList[0]; }
    }
}
