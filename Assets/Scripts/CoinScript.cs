using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    int coinValue;
    int coinQuality;
    //public Sprite[] coinType;

    private void Start()
    {
        //FIX INSTANT PICKUP SOMEHOW\\
        if (!GetComponent<Collider2D>().enabled)
            GetComponent<Collider2D>().enabled = true;

        //randomizes coin quality
        if (Random.value <= 0.5f)
            coinQuality = 0;
        else if (Random.value <= 0.85f)
            coinQuality = 1;
        else
            coinQuality = 2;

        //GetComponent<SpriteRenderer>().sprite = coinType[coinQuality];

        //sets value based on coin quality
        if (coinQuality == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            coinValue = 1;
        }
        else if (coinQuality == 1)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
            coinValue = 5;
        }
        else if (coinQuality == 2)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
            coinValue = 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        //if player pickup key, else push away from collision
        if (c.gameObject.name == "Player")
        {
            PlayerScript player = GameObject.Find("Player").GetComponent<PlayerScript>();
            player.coinCount += coinValue;
            Destroy(gameObject);
        }
        else
        {
            Vector3 dir = c.transform.position - transform.position;
            GetComponent<Rigidbody2D>().AddForce(-dir * 1f, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D c)
    {
        //if inside object push away from it
        Vector3 dir = c.transform.position - transform.position;
        GetComponent<Rigidbody2D>().AddForce(-dir * 1f, ForceMode2D.Impulse);
    }
}
