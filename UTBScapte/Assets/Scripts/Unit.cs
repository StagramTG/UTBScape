using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : HexUnit {

    public const int ARROWDAMAGE = 15;

    public CharacterClasses classe;
    public CharacterSpecies specie;

    public bool actionPossible = true;

    public LifeSlider lifeSlider;

    public int maxLife;
    public int life;

    private int damage;
    private Team team;

    public HexCell Location
    {
        get
        {
            return location;
        }
        set
        {
            if (location)
            {
                Grid.DecreaseVisibility(location, VisionRange, isPlayerUnit);
                location.Unit = null;
            }
            location = value;
            value.Unit = this;
            Grid.IncreaseVisibility(value, VisionRange, isPlayerUnit);
            transform.localPosition = value.Position;
        }
    }


    public void Init()
    {
        life = classe.BaseLife + specie.LifeBoost;
        damage = classe.BaseDamage + specie.DamageBoost;
        maxLife = life;
        lifeSlider.InitValue(life);
    }

    public void SetTeam(Team pTeam)
    {
        this.team = pTeam;
        isPlayerUnit = team.GetTeamType() == Team.Type.PLAYER;

        if (!isPlayerUnit)
            gameObject.SetActive(false);
    }

    public void TakeDamage(int pDamage)
    {
        life -= pDamage;
        lifeSlider.UpdateValue(life);

        if (life <= 0)
        {
            team.units.Remove(this);
            if (team.units.Count == 0)
                SceneManager.LoadScene(0);
            team = null;
            this.Die();
        }
    }

    public void ReceivedArrow()
    {
        this.TakeDamage(ARROWDAMAGE);
    }

    public int getDamage()
    {
        return this.damage;
    }
}
