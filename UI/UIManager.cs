using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager s { get; private set; }

    public GameObject onBattleIndicators;

    public Image HealthBar;
    public Image StaminaBar;
    public Image fatigueBar;
    public Image hungerBar;

    public Color[] uiColors;

    private void Awake() { s = this; }

    private void Update()
    {
        onBattleIndicators.SetActive(MainPanel.s.mainPanelDisabled);
    }
}
