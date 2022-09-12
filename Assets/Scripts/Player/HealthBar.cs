using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [HideInInspector] public Slider slider;

    private void Start()
    {
        PlayerScript player = GameObject.Find("Player").GetComponent<PlayerScript>();
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = player.maxHealth;
        slider.value = player.health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
