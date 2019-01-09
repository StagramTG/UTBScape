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

    public void Init(List<CharacterClasses> pclasses)
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
                int startX, startY;
                HexCell cell;
                do
                {
                    startX = Random.Range(1, 18);
                    startY = Random.Range(1, 13);
                    cell = grid.GetCell(HexCoordinates.FromOffsetCoordinates(startX, startY));
                } while (cell.IsUnderwater || cell.Unit != null);

                currentTeam.CreateUnit(0, pclasses[Random.Range(0, pclasses.Count)], cell);
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
        foreach (Unit unit in activeTeam.units)
        {
            unit.ResetSpeed();
            unit.actionPossible = true;
        }

        if (activeTeam.GetTeamType() == Team.Type.AI)
        {
            activeTeam.AIInterraction();
            nextActiveTeam();
        }
    }

    public void nextActiveTeam()
    {
        ++activeTeamTurnIndex;
        if (activeTeamTurnIndex >= teams.Count)
            activeTeamTurnIndex = 0;

        changeActiveTeam(activeTeamTurnIndex);
    }

    public int GetAliveUnitsForTeam(Team pTeam)
    {
        return pTeam.units.Count;
    }

    public int GetAliveUnitsForCurrentTeam()
    {
        return GetAliveUnitsForTeam(activeTeam);
    }

    public int GetAliveUnitsForOpponenetTeam()
    {
        return GetAliveUnitsForTeam(teams[1]);
    }
}
