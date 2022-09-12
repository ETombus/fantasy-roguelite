using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            ShootScript weapon = GameObject.Find("Weapon").GetComponent<ShootScript>();
            weapon.damage++;
            //weapon.activeSprite = 1;
            Destroy(gameObject);
        }
    }
}
