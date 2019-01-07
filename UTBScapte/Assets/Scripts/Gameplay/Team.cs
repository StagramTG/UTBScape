using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public enum Type
    {
        PLAYER,
        AI
    }

    public List<Unit> units;

    private int currentUnitIndex;
    private HexGrid grid;
    private CharacterSpecies specie;
    private Type type;

    public void Init(HexGrid pGrid, CharacterSpecies pSpecie, Type pType)
    {
        this.grid = pGrid;
        this.specie = pSpecie;
        this.type = pType;

        units = new List<Unit>();
        currentUnitIndex = 0;
    }

    public CharacterSpecies GetSpecie()
    {
        return specie;
    }

    public Type GetTeamType()
    {
        return type;
    }

    public void CreateUnit(int pPrefabIndex, HexCell cell)
    {
        if (pPrefabIndex < specie.unitsPrefabs.Count)
        {
            GameObject toInstanciate = specie.unitsPrefabs[pPrefabIndex];
            GameObject go = Instantiate(toInstanciate);
            Unit unit = go.GetComponent<Unit>();

            unit.SetTeam(this);

            grid.AddUnit(unit, cell, Random.Range(0f, 360f));

            // Add instiated unit to team
            units.Add(unit);
        }
    }

    public Unit getCurrentUnit()
    {
        return units[currentUnitIndex];
    }

    public Unit NextUnit()
    {
        ++currentUnitIndex;
        if (currentUnitIndex >= units.Count)
            currentUnitIndex = 0;

        return units[currentUnitIndex];
    }

    public Unit PreviousUnit()
    {
        --currentUnitIndex;
        if (currentUnitIndex < 0)
            currentUnitIndex = units.Count - 1;

        return units[currentUnitIndex];
    }
}
