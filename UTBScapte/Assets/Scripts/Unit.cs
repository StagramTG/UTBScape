using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : HexUnit {

    public CharacterClasses classe;
    public CharacterSpecies specie;

    private int life;
    private int damage;
    private bool moved;
    private Team team;

    private void Start()
    {
        life = classe.BaseLife + specie.LifeBoost;
        damage = classe.BaseDamage + specie.DamageBoost;
        moved = false;
    }

    public void SetTeam(Team pTeam)
    {
        this.team = pTeam;
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

    public bool getMoved()
    {
        return moved;
    }

    public void setMoved(bool pMoved)
    {
        moved = pMoved;
    }
}
