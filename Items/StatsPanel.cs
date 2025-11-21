using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour
{
    public enum Profession
    {
        None,
        OneHanded,
        TwoHanded,
    }

    private void Update()
    {
        //transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = InventoryManager.s.targetCharacter.level.ToString();
        transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = InventoryManager.s.targetCharacter.experience.ToString("0") + " / " + InventoryManager.s.targetCharacter.maxExperience.ToString("0");
        transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = InventoryManager.s.targetCharacter.health.ToString("0") + " / " + InventoryManager.s.targetCharacter.maxHealth.ToString("0");
        transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = InventoryManager.s.targetCharacter.stamina.ToString("0") + " / " + InventoryManager.s.targetCharacter.maxStamina.ToString("0");
        transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = InventoryManager.s.targetCharacter.damage.ToString("0");
        transform.GetChild(4).transform.GetChild(1).GetComponent<Text>().text = InventoryManager.s.targetCharacter.defence.ToString("0.#");
        transform.GetChild(5).transform.GetChild(1).GetComponent<Text>().text = InventoryManager.s.targetCharacter.armour.ToString("0");
        transform.GetChild(6).transform.GetChild(1).GetComponent<Text>().text = InventoryManager.s.targetCharacter.speedMultiplier.ToString("0.##");
        transform.GetChild(7).transform.GetChild(1).GetComponent<Text>().text = InventoryManager.s.targetCharacter.wage.ToString("0");
    }
}