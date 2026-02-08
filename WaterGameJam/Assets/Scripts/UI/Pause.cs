using UnityEngine;
using UnityEngine.InputSystem;


public class Pause : MonoBehaviour
{
    public bool gameIsPaused = false;
    public Canvas pauseMenu;

    public void openPauseMenu()
    {
        if (gameIsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void PauseGame()
    {
        pauseMenu.enabled=true;
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
