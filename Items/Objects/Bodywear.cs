using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodywear : MonoBehaviour
{
    private SkinnedMeshRenderer rend;
    private Character character;

    private void Start()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
        character = GetComponentInParent<Character>();
    }
    private void Update()
    {
        if((character.equipments[2].itemID==19 || character.equipments[2].itemID == 20 || character.equipments[2].itemID == 21 || character.equipments[2].itemID == 22))
        {
            Material[] newMaterials = rend.sharedMaterials;
            newMaterials[0] = ItemFunctions.emptyMaterial;
            rend.sharedMaterials = newMaterials;
        }
        else
        {
            if (character.equipments[3].itemID == 0)
            {
                rend.sharedMaterials = ItemFunctions.emptyChar.transform.GetChild(1).transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().sharedMaterials;
            }
            else
            {
                rend.sharedMaterials = ItemFunctions.armours.transform.GetChild(character.equipments[3].itemID - 1).GetComponent<SkinnedMeshRenderer>().sharedMaterials;
            }
        }
    }
}
