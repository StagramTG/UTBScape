using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : HexUnit {

    public CharacterClasses classe;
    public CharacterSpecies specie;

    public bool actionPossible = true;

    private int life;
    private int damage;
    private Team team;

    private void Start()
    {
    }

    public void Init()
    {
        life = classe.BaseLife + specie.LifeBoost;
        damage = classe.BaseDamage + specie.DamageBoost;
    }

    public void SetTeam(Team pTeam)
    {
        this.team = pTeam;
        isPlayerUnit = team.GetTeamType() == Team.Type.PLAYER;

        if (!isPlayerUnit)
            gameObject.SetActive(false);
    }

    public void takeDamage(int pDamage)
    {
        life -= pDamage;
        if (life <= 0)
        {
            team.units.Remove(this);
            team = null;
            this.Die();
        }
    }

    public int getDamage()
    {
        return this.damage;
    }
}
