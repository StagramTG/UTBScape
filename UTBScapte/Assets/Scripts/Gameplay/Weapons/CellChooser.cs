using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChooser : MonoBehaviour
{
    private Unit unit;

	public void Init(Unit punit)
    {
        unit = punit;

        // Search for adjacent cells
        List<HexCell> neighbourCells = new List<HexCell>();
        for(HexDirection dir = HexDirection.NE; dir < HexDirection.NW; ++dir)
        {
            HexCell current = unit.Location.GetNeighbor(dir);
            if(current.Unit != null)
            {
                neighbourCells.Add(current);
            }
        }
    }
}
