using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Let's try that again.
    public void RestartingGame()
    {
        SceneManager.LoadScene("MainGame");
        Debug.Log("Player has restarted the level.");
    }

    // I give up.
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Debug.Log("Player has gone back to main menu.");
    }

    // No really, I give up.
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("The application has quit.");
    }
}