using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public bool isMimic = false;
    public bool locked = false;

    float healthPotDroprate = 0.7f;
    float keyDroprate = 0.3f;

    int healthPot = 0;
    int key = 1;
    int coin = 2;

    GameObject player;
    SpriteRenderer sR;

    public Sprite chestOpened;
    public GameObject mimic;
    public GameObject[] pickups;
    public GameObject[] items;
    public GameObject[] wearableItems;

    private void Start()
    {
        //15% chance of mimic and assigns variables
        if (Random.value > 0.85f)
            isMimic = true;
        sR = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }
    //FIX NOFACE CHECK CHEST
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + other.gameObject.tag);
        if (other.gameObject.tag == "Projectile" && isMimic)
        {
            Instantiate(mimic, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name == "Player")
        {
            if (isMimic)
            {
                Instantiate(mimic, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            //golden chests
            else if (locked)
            {
                if (player.GetComponent<PlayerScript>().keyCount > 0)
                {
                    sR.sprite = chestOpened;
                    player.GetComponent<PlayerScript>().keyCount--;

                    //75% chance of active item, 35% chance of wearable item
                    if (Random.value > 0.35)
                    {
                        Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        //Instantiate(wearableItems[Random.Range(0, wearableItems.Length)]);
                    }
                }
                else
                    return;
            }
            //regular chests
            else
            {
                sR.sprite = chestOpened;
                //spawns between 1-5 pickups
                for (int i = Random.Range(1, 5); i > 0; i--)
                {
                    //spawns pickup according to random value and droprates
                    float chance = Random.value;
                    GameObject temp;

                    if (chance >= healthPotDroprate)
                        temp = Instantiate(pickups[healthPot], transform.position, Quaternion.identity);
                    else if (chance <= keyDroprate)
                        temp = Instantiate(pickups[key], transform.position, Quaternion.identity);
                    else
                        temp = Instantiate(pickups[coin], transform.position, Quaternion.identity);

                    //FIX THIS SHIT CODE, OMFG DUDE\\ //(TRY SWITCH playerpos & temppos)
                    Vector3 dir = player.transform.position - temp.transform.position;
                    temp.GetComponent<Collider2D>().enabled = false;
                    temp.GetComponent<Rigidbody2D>().AddForce(-dir * Random.Range(5, 10), ForceMode2D.Impulse);
                }
            }
            //makes chest lighter and disables script
            GetComponent<Rigidbody2D>().drag = 5f;
            Destroy(gameObject.GetComponent<ChestScript>());
        }
    }
}
