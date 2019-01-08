using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClasseTypes
{
    NONE,
    WARRIOR,
    ARCHER
}

[CreateAssetMenu(fileName = "Classe", menuName = "UTBMScape/Gameplay/Classe", order = 1)]
public class CharacterClasses : ScriptableObject 
{
	public ClasseTypes Name = ClasseTypes.NONE;
	public int BaseLife = 100;
	public int BaseDamage = 10;
}