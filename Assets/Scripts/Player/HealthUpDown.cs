using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpDown : MonoBehaviour
{
    public float healthUpDownAmount;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerScript>().maxHealth += healthUpDownAmount;
            HealthBar healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            healthBar.slider.maxValue += healthUpDownAmount;
            Destroy(gameObject);
        }
    }
}
