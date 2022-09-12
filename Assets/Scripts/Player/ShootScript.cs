using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    float horizontalRotation;
    float verticalRotation;
    float projectiles = 0.75f;
    bool weaponloaded = true;

    AudioSource weaponShotSound;
    SpriteRenderer weapon;
    Transform firepoint;

    public GameObject projectile;
    private GameObject player;

    [System.NonSerialized]
    public float damage = 2.5f;
    public float shotSpeed = 1f;

    //public int activeSprite = 0;
    //public Sprite[] projectileSprites;

    void Start()
    {
        firepoint = GameObject.Find("Firepoint").transform;
        player = GameObject.Find("Player");
        weaponShotSound = GetComponent<AudioSource>();
        weapon = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        verticalRotation = Input.GetAxisRaw("Fire1");
        horizontalRotation = Input.GetAxisRaw("Fire2");

        //rotates crossbow between 8 45° angles according to inputs 
        Vector2 crossbowRotation = new Vector2(horizontalRotation, verticalRotation);
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, crossbowRotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 1000000 * Time.deltaTime);

        //flips weapon if aiming to the left
        if((transform.localEulerAngles.z >= 90) && (transform.localEulerAngles.z < 270 ))
            weapon.flipY = true;
        else
            weapon.flipY = false;

        //shoots weapon if loaded then reloads
        if (((Input.GetAxisRaw("Fire1") != 0) || (Input.GetAxisRaw("Fire2") != 0)) && weaponloaded)
        {
            ShootProjectile();
            weaponloaded = false;
            StartCoroutine(Reload());
        }
    }

    void ShootProjectile()
    {
        //fires projectile in direction of weapon and automatically destroys after seconds
        weaponShotSound.Play();
        var shot = Instantiate(projectile, firepoint.position, firepoint.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = transform.right * shotSpeed * 10f;
        shot.GetComponent<Rigidbody2D>().velocity += player.GetComponent<Rigidbody2D>().velocity;
        var dir = shot.GetComponent<Rigidbody2D>().velocity;
        var rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shot.GetComponent<Transform>().rotation = Quaternion.AngleAxis(rot, Vector3.forward);

        Destroy(shot, 3f);
        //ChangeSprite(activeSprite);
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(projectiles);
        weaponloaded = true;
    }

    //MAYBE ADD CHANGEABLE SPRITE\\
    /*public void ChangeSprite(int sprite)
    {
        SpriteRenderer pSR = GameObject.FindGameObjectWithTag("Projectile").GetComponent<SpriteRenderer>();
        pSR.sprite = projectileSprites[sprite];
    }*/
}
