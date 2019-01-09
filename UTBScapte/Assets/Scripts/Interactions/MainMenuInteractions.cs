using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuInteractions : MonoBehaviour
{
    public List<CharacterSpecies> species;

    /*private void Start()
    {
        PlayButtonPressed();
    }*/

    public void PlayButtonPressed()
    {
        Debug.Log("Play pressed !!!");

        // Fill data in GameInitData
        TeamDescriptor descPlayer = new TeamDescriptor();
        descPlayer.type = Team.Type.PLAYER;
        descPlayer.species = species[0];

        TeamDescriptor descAI = new TeamDescriptor();
        descAI.type = Team.Type.AI;
        descAI.species = species[1];

        GameInitData.Reset();
        GameInitData.teamsDescriptions.Add(descPlayer);
        GameInitData.teamsDescriptions.Add(descAI);

        SceneManager.LoadScene(1);
    }

    public void QuitButtonPressed()
    {
        Debug.Log("Quit pressed !!!");
        Application.Quit();
    }
}