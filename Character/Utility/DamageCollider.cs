using System.Collections;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [HideInInspector] public WeaponObject weapon;
    [HideInInspector] public CharacterCombat com;

    private void Awake()
    {
        weapon = GetComponentInParent<WeaponObject>();
        com = GetComponentInParent<CharacterCombat>();
    }
}
