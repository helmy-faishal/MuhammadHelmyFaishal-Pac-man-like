using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    public static SceneSwitching instance;

    private void Awake()
    {
        instance = this;
    }

    public void ResetCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SwitchScene(string sceneName)
    {
        ResetCursor();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadGameplay()
    {
        SwitchScene("Gameplay");
    }

    public void LoadMainMenu()
    {
        SwitchScene("MainMenu");
    }

    public void LoadWinScene()
    {
        SwitchScene("WinScene");
    }

    public void LoadLoseScene()
    {
        SwitchScene("LoseScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
