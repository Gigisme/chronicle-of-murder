using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool isPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    private bool cursorLocked = false;

    private void Start()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        ToggleCursor();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        ToggleCursor();
    }
    
    private void ToggleCursor()
    {
        cursorLocked = !cursorLocked;
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
    
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
