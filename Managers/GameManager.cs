using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager s { get; private set; }
    
    public LayerMask groundLayer;

    public float timeScale;

    public bool hideCursor;

    private void Awake() { s = this; }

    private void Start()
    {
        if (!SceneManager.GetSceneByBuildIndex(1).isLoaded) { SceneManager.LoadScene(1,LoadSceneMode.Additive); }
    }

    private void Update()
    {
        if (MainPanel.s.mainPanelDisabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
