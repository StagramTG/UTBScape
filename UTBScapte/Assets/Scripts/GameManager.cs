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

        HexCell cell = grid.GetCell(new HexCoordinates(10, 7));

        Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
        player.trackingOriginTransform.position = cell.transform.position + playerFeetOffset;

        //cameraVR.transform.position = cell.transform.position;

        currentUnit = Instantiate(startUnit);
        grid.AddUnit(currentUnit, cell, Random.Range(0f, 360f));
        currentUnit.gameObject.SetActive(false);
        teleport.setCurrentUnit(currentUnit);

        // Init all teams with members and turn index
        teamManager.Init();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            currentUnit.Location = grid.GetCell(player.trackingOriginTransform.position);
            currentUnit.setMoved(true);
            teleport.gameObject.SetActive(false);
        }
    }
}
