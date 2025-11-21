using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    public static MainPanel s { get; private set; }

    public bool mainPanelDisabled;

    public int panelMode; //0 inventory 


    [SerializeField] private GameObject blurVolume;

    private void Awake() { s = this; }

    private void Start()
    {
        transform.localScale = new Vector2(0, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && panelMode != 1)
        {
            panelMode = 0;

            HandleMainPanel();
        }
    }

    public void HandleMainPanel()
    {
        mainPanelDisabled = !mainPanelDisabled;

        if (mainPanelDisabled)
        {
            transform.localScale = new Vector2(0, 0);
            blurVolume.SetActive(false);

            UpdatePanels();
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            blurVolume.SetActive(true);

            UpdatePanels();
        }
    }

    public void UpdatePanels()
    {
        switch (panelMode)
        {
            case 0: { UpdateInventory(); break; }
        }
    }

    public void UpdateInventory()
    {
        if (mainPanelDisabled)
        {
            InventoryManager.s.whichCharacter = 0;
            InventoryManager.s.targetContainer[1] = null;
            InventoryManager.s.contID = 0;
            InventoryManager.s.DestroyInventory();
        }
        else
        {
            InventoryManager.s.SetUpInventory(Item.ItemClass.Null);
        }
    }
}
