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
    public int turnIndex;

	void Start () {
        units = new List<Unit>();
        unitsCount = 0;
        turnIndex = 0;
	}
}
