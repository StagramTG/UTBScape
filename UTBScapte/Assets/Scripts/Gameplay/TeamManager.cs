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

    public HexGrid grid;

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
            currentTeam.Init(grid, desc.species, desc.type);

            // createUnits
            for (int i = 0; i < unitsQuantityByTeam; ++i)
            {
                // Instantiate unit
                HexCell cell = grid.GetCell(new HexCoordinates(8 + i, 7));
                currentTeam.CreateUnit(0, cell);
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
        activeTeam = teams[pindex];

        //Reset movement for all units
        foreach(Unit unit in activeTeam.units)
        {
            unit.setMoved(false);
        }
    }

    public void nextActiveTeam()
    {
        ++activeTeamTurnIndex;
        if (activeTeamTurnIndex >= teams.Count)
            activeTeamTurnIndex = 0;

        changeActiveTeam(activeTeamTurnIndex);
    }
}
