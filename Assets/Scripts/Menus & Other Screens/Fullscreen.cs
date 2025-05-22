using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fullscreen : MonoBehaviour
{
    public CanvasScaler canvasScaler;

    public bool fullscreenEnabled = true;

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    void Start()
    {
        // Initial fullscreen state
        Screen.fullScreen = fullscreenEnabled;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            ToggleFullscreen();
            Debug.Log("Player has hit F11 and adjusted the full screen.");
        }
    }
}