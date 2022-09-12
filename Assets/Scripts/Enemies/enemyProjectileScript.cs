using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectileScript : MonoBehaviour
{
    float damage;
    //float shotSpeed;

    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        damage = GetComponentInParent<EnemyShootScript>().damage;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            //if collide with player initiate TakeDamage
            PlayerScript player = GameObject.Find("Player").GetComponent<PlayerScript>();
            player.StartCoroutine(player.TakeDamage(damage));
            Destroy(gameObject);
        }
    }
}
