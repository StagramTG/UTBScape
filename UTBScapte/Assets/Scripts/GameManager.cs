using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour {

    public MapManager mapManager;
    //public GameObject cameraVR;
    public Teleport teleport;
    public Unit startUnit;

    public TeamManager teamManager;

    private HexGrid grid;
    private Unit currentUnit;
    private Player player;

	void Start() {
        grid = mapManager.InitMap();
        player = Player.instance;

        // Init all teams with members and turn index
        teamManager.Init();

        SetCurrentUnit(teamManager.activeTeam.units[0]);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            currentUnit.Location = grid.GetCell(player.trackingOriginTransform.position);
            currentUnit.setMoved(true);
            teleport.gameObject.SetActive(false);
        }

        if (Input.GetButtonDown("EndTurn"))
        {
            Debug.Log("End Turn");
            teamManager.nextActiveTeam();
            SetCurrentUnit(teamManager.activeTeam.getCurrentUnit());
        }

        if (Input.GetButtonDown("UnitBefore"))
        {
            SetCurrentUnit(teamManager.activeTeam.PreviousUnit());
        }
        else if (Input.GetButtonDown("UnitAfter"))
        {
            SetCurrentUnit(teamManager.activeTeam.NextUnit());
        }
    }

    private void SetCurrentUnit(Unit pUnit)
    {
        //Reactivate the previous unit
        if (currentUnit != null)
            currentUnit.gameObject.SetActive(true);

        currentUnit = pUnit;
        currentUnit.gameObject.SetActive(false);
        teleport.setCurrentUnit(currentUnit);

        //Check if the unit have already moved
        if (currentUnit.getMoved())
            teleport.gameObject.SetActive(false);
        else
            teleport.gameObject.SetActive(true);

        //Place player at the right position
        Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
        player.trackingOriginTransform.position = pUnit.Location.transform.position + playerFeetOffset;
    }
}
