using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuInteractions : MonoBehaviour
{
    public CharacterSpecies defaultSpecies;

    public void PlayButtonPressed()
    {
        Debug.Log("Play pressed !!!");

        // Fill data in GameInitData
        TeamDescriptor desc = new TeamDescriptor();
        desc.type = Team.Type.PLAYER;
        desc.species = defaultSpecies;

        GameInitData.Reset();
        GameInitData.teamsDescriptions.Add(desc);

        SceneManager.LoadScene(1);
    }

    public void QuitButtonPressed()
    {
        Debug.Log("Quit pressed !!!");
        Application.Quit();
    }

    private void Start()
    {
        PlayButtonPressed();
    }
}
