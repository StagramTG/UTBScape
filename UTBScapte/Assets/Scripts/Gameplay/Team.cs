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
    public int unitsCount;

    public void Init()
    {
        units = new List<Unit>();
        unitsCount = 0;
    }
}
