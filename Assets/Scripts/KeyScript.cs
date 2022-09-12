using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
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
            PlayerScript player = GameObject.Find("Player").GetComponent<PlayerScript>();
            player.keyCount++;
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
