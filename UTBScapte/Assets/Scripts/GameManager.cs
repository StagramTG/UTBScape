using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour {

    public MapManager mapManager;
    public GameObject cameraVR;
    public Teleport teleport;

    private HexGrid grid;
    private HexUnit currentUnit;

	void Start() {
        grid = mapManager.InitMap();

        HexCell cell = grid.GetCell(new HexCoordinates(10, 7));
        cameraVR.transform.position = cell.transform.position;

        grid.AddUnit(Instantiate(HexUnit.unitPrefab), cell, Random.Range(0f, 360f));
        currentUnit = cell.Unit;
        teleport.setCurrentUnit(currentUnit);
    }
}
