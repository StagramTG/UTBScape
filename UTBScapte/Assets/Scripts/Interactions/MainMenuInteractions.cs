using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MainMenuInteractions : MonoBehaviour
{
    public void PlayButtonPressed()
    {
        Debug.Log("Play pressed !!!");
    }

    public void QuitButtonPressed()
    {
        Debug.Log("Quit pressed !!!");
        Application.Quit();
    }
}
