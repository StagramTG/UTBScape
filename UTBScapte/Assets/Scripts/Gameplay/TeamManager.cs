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
    // Number of units in each team at game's start
    public int unitsQuantityByTeam = 3;

	void Awake ()
    {
        teams = new List<Team>();
        activeTeam = null;
        activeTeamTurnIndex = 0;
	}

    public void Init()
    {
        // Init all teams by using data feed "GameInitData" (Static class)
        foreach(TeamDescriptor desc in GameInitData.teamsDescriptions)
        {
            // Create and instantiate team
            GameObject currentTeamGO = Instantiate(teamPrefab);
            Team currentTeam = currentTeamGO.GetComponent<Team>();
            currentTeam.Init();

            // createUnits
            for (int i = 0; i < unitsQuantityByTeam; ++i)
            {
                // Instantiate unit
                GameObject toInstanciate = desc.species.unitsPrefabs[0];
                GameObject go = Instantiate(toInstanciate);

                // Add instiated unit to team
                currentTeam.units.Add(go.GetComponent<Unit>());
            }

            // Add newly created team to manager list
            AddTeam(currentTeam);
        }

        activeTeam = teams[activeTeamTurnIndex];
    }

    public void AddTeam(Team pteam)
    {
        teams.Add(pteam);
    }

    public void changeActiveTeam(int pindex)
    {

    }

    public void nextActiveTeam()
    {

    }
}
