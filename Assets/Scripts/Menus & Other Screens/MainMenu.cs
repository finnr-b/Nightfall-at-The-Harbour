using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // It's time to play the game.
    public void StartGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    // Let's tweak some stuff.
    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    // Time to play something else.
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("The application has quit.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("The application has quit.");
        }
    }
}