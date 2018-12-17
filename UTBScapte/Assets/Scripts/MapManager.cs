using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public HexGrid grid;
    public HexMapGenerator mapGenerator;

    public Material terrainMaterial;

    int mapX = 20;
    int mapY = 15;

    public HexGrid InitMap()
    {
        grid.Init(mapX, mapY);
        mapGenerator.GenerateMap(mapX, mapY);

        Shader.DisableKeyword("HEX_MAP_EDIT_MODE");
        terrainMaterial.DisableKeyword("GRID_ON");

        return grid;
    }
}
