using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public float health;
    public GameObject slider;

    private void Start()
    {
        slider.GetComponent<Slider>().maxValue = health;
        slider.GetComponent<Slider>().value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        slider.GetComponent<Slider>().value = health;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        //Death Effect
        Destroy(gameObject);
    }
}
