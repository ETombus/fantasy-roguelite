using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : MonoBehaviour
{
    public float healAmount;

    private void Start()
    {
        //FIX INSTANT PICKUP SOMEHOW\\
        if (!GetComponent<Collider2D>().enabled)
            GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        //if player pickup key, else push away from collision
        if (c.gameObject.name == "Player")
        {
            //stops health from going above max HP
            PlayerScript player = GameObject.Find("Player").GetComponent<PlayerScript>();
            if (gameObject.name == "MaxHealthPot")
                player.health = player.maxHealth;
            else
            {
                if (player.health <= player.maxHealth - healAmount)
                    player.health += healAmount;
                else
                    player.health = player.maxHealth;
            }

            //updates healthbar
            HealthBar healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            healthBar.SetHealth(player.health);
            Destroy(gameObject);
        }
        else
        {
            Vector3 dir = c.transform.position - transform.position;
            GetComponent<Rigidbody2D>().AddForce(-dir * 2f, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D c)
    {
        //if inside object push away from it
        Vector3 dir = c.transform.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(-dir * 1f, ForceMode2D.Impulse);
    }
}
