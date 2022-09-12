using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    Rigidbody2D rb;
    float shotSpeed;
    float damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //take damage form weapon
        damage = GameObject.Find("Weapon").GetComponent<ShootScript>().damage;
    }

    private void OnTriggerEnter2D(Collider2D contact)
    {
        //if collide with enemy do damage and break else just break
        if (contact.gameObject.tag == "Enemy")
        {
            EnemyHealthScript enemy = contact.gameObject.GetComponent<EnemyHealthScript>();
            enemy.TakeDamage(damage * 10);
            Destroy(gameObject);
        }
        else if (contact.gameObject.tag == "Player" || contact.gameObject.tag == "Interactable" || contact.gameObject.tag == "Projectile") { }
        else
        {
            Destroy(gameObject);
        }

        //ADD INTERACTABLE ENVIRONMENT\\
        /*
        else if (interactable environment)
            Break(); // loot chance (Luck?)
            Destroy(gameobject);        
        */
    }


    // ADD RANGE LIMIT\\
    /*IEnumerator ProjectileRangeLimit()
    {
        Debug.Log("for i");
        yield return new WaitForSeconds(1);
        for (int i = 10; i < 0; i--)
        {
            Debug.Log("for i");
            rb.velocity = transform.right * shotSpeed * i;
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }*/
}
