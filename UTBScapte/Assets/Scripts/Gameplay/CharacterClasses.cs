using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Classe", menuName = "UTBMScape/Gameplay/Classe", order = 1)]
public class CharacterClasses : ScriptableObject 
{
	public string Name = "Class name";
	public int BaseLife = 100;
}