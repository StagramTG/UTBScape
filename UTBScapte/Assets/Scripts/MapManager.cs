using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public HexGrid grid;
    public HexMapGenerator mapGenerator;
    public GameObject cameraVR;

    public Material terrainMaterial;

    int mapX = 20;
    int mapY = 15;

    void Start()
    {
        grid.Init(mapX, mapY);
        mapGenerator.GenerateMap(mapX, mapY);

        HexCell cell = grid.GetCell(new HexCoordinates(10, 7));
        cameraVR.transform.position = cell.transform.position;

        Shader.EnableKeyword("HEX_MAP_EDIT_MODE");
        terrainMaterial.DisableKeyword("GRID_ON");
    }
}
