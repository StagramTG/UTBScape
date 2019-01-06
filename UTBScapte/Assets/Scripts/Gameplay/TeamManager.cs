using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    // Team prefab to instantiate in the world for each team
    public GameObject teamPrefab;

    // Contains all teams for current game
    public List<Team> teams;
    // Team currently playing (turn system)
    public Team activeTeam;
    // Index of the team currently playing
    public int activeTeamTurnIndex;

    public int unitsQuantityByTeam = 3;

	void Start ()
    {
        teams = new List<Team>();
        activeTeam = null;
	}

    public void Init()
    {
        // Init all teams by using data feed "GameInitData" (Static class)
        foreach(TeamDescriptor desc in GameInitData.teamsDescriptions)
        {
            // Create and instantiate team
            GameObject currentTeamGO = Instantiate(teamPrefab);
            Team currentTeam = currentTeamGO.GetComponent<Team>();

            // createUnits
            for(int i = 0; i < unitsQuantityByTeam; ++i)
            {

            }

            // Add newly created team to manager list
            AddTeam(currentTeam);
        }
    }

    public void AddTeam(Team pteam)
    {
        teams.Add(pteam);
    }

    public void changeActiveTeam()
    {

    }

    public void nextActiveTeam()
    {

    }
}
