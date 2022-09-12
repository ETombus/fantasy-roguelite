using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamageScript : MonoBehaviour
{
    bool tick = true;
    public float contactDamage;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D contact)
    {
        if (contact.gameObject.name == "Player")
        {
            //gets collision direction and does contact damage
            Vector3 dir = contact.transform.position - transform.position;
            player.GetComponent<PlayerScript>().ContactDMG(contactDamage, dir);
        }
    }

    /*IEnumerator TickDamage()
    {
        if (tick)
            tick = false;
        yield return new WaitForSeconds(0.75f);
        tick = true;
    }*/
}
