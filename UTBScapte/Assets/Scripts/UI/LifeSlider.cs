using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSlider : MonoBehaviour {

    private Image img;
    private Slider slider;

    private Color maxLifeColor = Color.green;
    private Color minLifeColor = Color.red;

    private int maxLife;

	public void InitValue(int life)
    {
        img = transform.GetComponentsInChildren<Image>()[1];
        slider = transform.GetComponentInChildren<Slider>();

        slider.minValue = 0;
        slider.maxValue = life;
        slider.value = life;
        img.color = maxLifeColor;
        maxLife = life;
    }

    public void UpdateValue(int life)
    {
        slider.value = life;
        img.color = Color.Lerp(maxLifeColor, minLifeColor, (maxLife - life) * 1f / maxLife);
    }
}
