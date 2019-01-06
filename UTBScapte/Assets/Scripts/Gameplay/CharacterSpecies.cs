using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Species", menuName = "UTBMScape/Gameplay/Species", order = 2)]
public class CharacterSpecies : ScriptableObject 
{
	public string Name = "Species name";
	public int LifeBoost = 10;
	public int DamageBoost = 2;

    public List<GameObject> unitsPrefabs;
}