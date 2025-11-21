using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class WeaponsData : MonoBehaviour
{
    public static WeaponsData s { get; private set; }

    public List<Weapon> weapons;

    private void Awake() 
    { 
        s = this;

        string[] lines = Resources.Load<TextAsset>("WeaponsData").text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] splitData = lines[i].Split(',');
            Weapon weapon = new Weapon();
            weapon.weaponClass = splitData[0];
            int.TryParse(splitData[1], NumberStyles.Any, CultureInfo.InvariantCulture, out weapon.weaponType);
            int.TryParse(splitData[2], NumberStyles.Any, CultureInfo.InvariantCulture, out weapon.scabbardType);
            int.TryParse(splitData[3], NumberStyles.Any, CultureInfo.InvariantCulture, out weapon.staminaConsumption);
            float.TryParse(splitData[4], NumberStyles.Any, CultureInfo.InvariantCulture, out weapon.weaponSpeed);
            float.TryParse(splitData[5], NumberStyles.Any, CultureInfo.InvariantCulture, out weapon.attackDistance);

            weapons.Add(weapon);
        }
    }
}

[System.Serializable]
public class Weapon
{
    public string weaponClass;

    public int weaponType;
    public int scabbardType;
    public int staminaConsumption;

    public float weaponSpeed;
    public float attackDistance;

    public Weapon(string clas, int type, int scabbard,int stamina, float speed, float distance)
    {
        weaponClass = clas;
        weaponType = type;
        scabbardType = scabbard;
        staminaConsumption = stamina;
        weaponSpeed = speed;
        attackDistance = distance;
    }
    
    public Weapon() { }
}