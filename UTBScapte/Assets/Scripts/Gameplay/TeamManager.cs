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

	void Start ()
    {
        teams = new List<Team>();
        activeTeam = null;
	}

    public void Init()
    {

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
