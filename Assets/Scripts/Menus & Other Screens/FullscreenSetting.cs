using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenSetting : MonoBehaviour
{
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Player has hit the toggle");
    }
}
