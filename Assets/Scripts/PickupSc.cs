using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSc : MonoBehaviour
{
    bool picked = false;

    void Start()
    {
        StartCoroutine(SpawnCooldown());
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        //if player picks up item, run pickup function, else push away from collision
        if (c.gameObject.name == "Player")
        {
            //start function in childSc
            picked = true;
        }
        else
        {
            Vector3 dir = c.transform.position - transform.position;
            GetComponent<Rigidbody2D>().AddForce(-dir * 2f, ForceMode2D.Impulse);
        }
    }

    IEnumerator SpawnCooldown()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        GetComponent<Collider2D>().enabled = true;
    }
}
