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

    public void CreateUnit(int pPrefabIndex, CharacterClasses pclasse, HexCell cell)
    {
        if (pPrefabIndex < specie.unitsPrefabs.Count)
        {
            GameObject toInstanciate = specie.unitsPrefabs[pPrefabIndex];
            GameObject go = Instantiate(toInstanciate);
            Unit unit = go.GetComponent<Unit>();

            unit.classe = pclasse;
            unit.Init();
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

    public void AIInterraction()
    {
        foreach (Unit unit in units)
        {
            List<Unit> visible = VisibleUnit(unit);
            if (visible.Count > 0)
            {
                bool cac = UnitCAC(unit, visible[0]);
                if (unit.classe.Name == ClasseTypes.ARCHER)
                {
                    ArcherAttaque(unit, visible[0]);
                    if (cac)
                        MoveOpposite(unit, visible[0]);
                }
                else
                {
                    if (cac)
                        visible[0].TakeDamage(unit.getDamage());
                    else
                        GoToCAC(unit, visible[0]);
                }
            }
            else
                MoveRandomly(unit);
        }
    }

    private void ArcherAttaque(Unit pUnit, Unit pTarget)
    {
        if (Random.Range(0, 100) > 80)// First arrow
            pTarget.ReceivedArrow();

        if (Random.Range(0, 100) > 80)// Second arrow
            pTarget.ReceivedArrow();
    }

    private void GoToCAC(Unit pUnit, Unit pTarget)
    {
        int minDistance = int.MaxValue;
        HexCell cell = null;
        for (HexDirection dir = HexDirection.NE; dir <= HexDirection.NW; ++dir)
        {
            HexCell tempCell = pTarget.Location.GetNeighbor(dir);
            if (EmptyCell(tempCell))
            {
                pUnit.Grid.FindPath(pUnit.Location, tempCell, pUnit);
                if (tempCell.Distance < minDistance)
                {
                    cell = tempCell;
                    minDistance = tempCell.Distance;
                }
            }
        }
        pUnit.Grid.FindPath(pUnit.Location, cell, pUnit);
        if ((cell.Distance - 1) / pUnit.Speed > 0)
        {
            List<HexCell> path = pUnit.Grid.GetPath();
            int i = 0;

            while((path[i].Distance - 1) / pUnit.Speed <= 0)
            {
                ++i;
            }
            --i;
            Move(pUnit, path[i]);
        }
        else
        {
            Move(pUnit, cell);
            pTarget.TakeDamage(pUnit.getDamage());
        }
    }

    private void MoveRandomly(Unit currentUnit)
    {
        List<HexCell> neighbors = new List<HexCell>();
        neighbors.Add(currentUnit.Location);
        for (HexDirection dir = HexDirection.NE; dir <= HexDirection.NW; ++dir)
        {
            if (currentUnit.Location.GetNeighbor(dir) != null && EmptyCell(currentUnit.Location.GetNeighbor(dir)))
                neighbors.Add(currentUnit.Location.GetNeighbor(dir));
        }
        HexCell dest = neighbors[Random.Range(0, neighbors.Count)];
        Move(currentUnit, dest);
    }

    private void Move(Unit pUnit, HexCell pDest)
    {
        pUnit.Location = pDest;

        if (!pDest.IsVisible)
            pUnit.gameObject.SetActive(false);
        else
            pUnit.gameObject.SetActive(true);
    }

    private bool UnitCAC(Unit pUnit1, Unit pUnit2)
    {
        for (HexDirection dir = HexDirection.NE; dir <= HexDirection.NW; ++dir)
        {
            HexCell neighbor = pUnit1.Location.GetNeighbor(dir);
            if (neighbor != null)
            {
                if (neighbor.Unit != null && neighbor.Unit == pUnit2)
                    return true;
            }
        }
        return false;
    }

    private void MoveOpposite(Unit pUnitToMove, Unit pUnit)
    {
        HexDirection direction = HexDirection.E;
        for (HexDirection dir = HexDirection.NE; dir <= HexDirection.NW; ++dir)
        {
            HexCell neighbor = pUnitToMove.Location.GetNeighbor(dir);
            if (neighbor != null)
            {
                if (neighbor.Unit != null && neighbor.Unit == pUnit)
                {
                    direction = dir;
                    break;
                }
            }
        }

        direction = direction.Opposite();
        if (EmptyCell(pUnitToMove.Location.GetNeighbor(direction)))
            Move(pUnitToMove, pUnitToMove.Location.GetNeighbor(direction));

        else if (EmptyCell(pUnitToMove.Location.GetNeighbor(direction.Next())))
            Move(pUnitToMove, pUnitToMove.Location.GetNeighbor(direction.Next()));

        else if (EmptyCell(pUnitToMove.Location.GetNeighbor(direction.Previous())))
            Move(pUnitToMove, pUnitToMove.Location.GetNeighbor(direction.Previous()));
    }

    private bool EmptyCell(HexCell pCell)
    {
        return !pCell.IsUnderwater && pCell.Unit == null && pCell.Explorable;
    }

    List<Unit> VisibleUnit(Unit currentUnit)
    {
        List<Unit> unitList = new List<Unit>();
        List<HexCell> visibleCell = currentUnit.Grid.GetVisibleCells(currentUnit.Location, currentUnit.VisionRange);

        foreach(HexCell cell in visibleCell)
        {
            if (cell.Unit != null && cell.Unit.specie != this.specie)
            {
                unitList.Add(cell.Unit);
            }
        }

        return unitList;
    }
}
