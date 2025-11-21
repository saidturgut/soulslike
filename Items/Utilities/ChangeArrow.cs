using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeArrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image img;

    private void Start()
    {
        img = GetComponent<Image>(); 
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && img.color != Color.black)
        {
            /*switch (name)
            {
                case "Left":

                    if (InventoryManager.s.whichCharacter == 0)
                    {
                        InventoryManager.s.whichCharacter = Player.s.party.partyMembers.Count - 1;
                    }
                    else
                    {
                        InventoryManager.s.whichCharacter--;
                    }
                    break;

                case "Right":

                    if (InventoryManager.s.whichCharacter == Player.s.party.partyMembers.Count - 1)
                    {
                        InventoryManager.s.whichCharacter = 0;
                    }
                    else
                    {
                        InventoryManager.s.whichCharacter++;
                    }
                    break;
            }*/
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        img.color = InventoryManager.s.inventoryColors[3];
    }

    public void OnPointerExit(PointerEventData data)
    {
        img.color = Color.black;
    }
}
