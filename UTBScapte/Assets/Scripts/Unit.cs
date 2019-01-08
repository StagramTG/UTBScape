using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : HexUnit {

    public CharacterClasses classe;
    public CharacterSpecies specie;

    public bool actionPossible = true;

    public Slider lifeSlider;
    public Image fillImage;

    private int life;
    private int maxLife;
    private int damage;
    private Team team;

    private Color maxLifeColor = Color.green;
    private Color minLifeColor = Color.red;

    private float lastUpdate = 0;

    private void Start()
    {
    }

    public void Init()
    {
        life = classe.BaseLife + specie.LifeBoost;
        maxLife = life;
        damage = classe.BaseDamage + specie.DamageBoost;

        lifeSlider.minValue = 0;
        lifeSlider.maxValue = life;
        lifeSlider.value = life;
        fillImage.color = maxLifeColor;
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

        lifeSlider.value = life;
        fillImage.color = Color.Lerp(maxLifeColor, minLifeColor, (maxLife - life) * 1f / maxLife);

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
