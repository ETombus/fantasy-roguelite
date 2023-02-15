using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    int layerMask = 1 << 2;
    bool shooting = false;

    Transform player;

    Quaternion rot;
    Vector3 enemyPos;
    RaycastHit2D hit;

    public float range;
    public float damage;
    [Tooltip("projectiles before reload")]
    public int magSize;
    [Tooltip("projectile travel speed")]
    public float shotSpeed;
    [Tooltip("delay between projectiles")]
    public float shotDelay;
    [Tooltip("read the name, you don't need a tooltip")]
    public float reloadTime;

    public GameObject projectile;
    public Transform firepoint;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        enemyPos = gameObject.transform.position;
        layerMask = ~layerMask;
    }
    
    void Update()
    {
        //looks at player, layermask filters out projectiles, if player in line of sight and not already shooting, shoot
        hit = Physics2D.Raycast(transform.position, player.position - transform.position, range, layerMask);
        if (hit.collider != null && hit.collider.gameObject.tag == "Player" && !shooting)
            StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        shooting = true;
        //fires as long as magazine isn't empty
        for (int i = magSize; i > 0; i--)
        {
            yield return new WaitForSeconds(shotDelay);
            //shoot anim

            //fires projectile with rotation of players direction
            var newProjectile = Instantiate(projectile, firepoint.position, 
                Quaternion.LookRotation(Vector3.forward, player.position - transform.position));
            newProjectile.transform.Rotate(0f, 0f, 90f);

            //makes projectile a child of the enemy
            newProjectile.transform.parent = gameObject.transform;

            //sets speed of projectile toward player
            newProjectile.GetComponent<Rigidbody2D>().velocity =
                (player.position - newProjectile.transform.position).normalized * shotSpeed;

            Destroy(newProjectile, 2f);
        }

        //if magasine empty reload
        yield return new WaitForSeconds(reloadTime);
        //reload anim
        shooting = false;
    }
}
