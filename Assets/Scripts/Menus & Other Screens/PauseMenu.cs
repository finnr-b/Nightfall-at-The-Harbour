using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;
	public GameObject ammoCount;
	public GameObject playerHealth;
	public static bool isPaused;

	// Start is called before the first frame update
	void Start()
	{
		pauseMenu.SetActive(false);
		ammoCount.SetActive(true);
		playerHealth.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPaused)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}
	}

	// Hold on a minute.
	public void PauseGame()
	{
		pauseMenu.SetActive(true);
		ammoCount.SetActive(false);
		playerHealth.SetActive(false);
		Time.timeScale = 0f;
		isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

	// Ok, back to it.
	public void ResumeGame()
	{
		pauseMenu.SetActive(false);
		ammoCount.SetActive(true);
		playerHealth.SetActive(true);
		Time.timeScale = 1f;
		isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

	// Let me try that differently
	public void RestartLevel()
    {
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainGame");
    }
	// Ragequit
	public void GoToMainMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("StartMenu");
	}

	// Ragequit 2
	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("The application has quit.");
	}
}