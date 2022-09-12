using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D body;

    float vertical;
    float horizontal;
    bool knockback = false;

    [HideInInspector] public float runSpeed = 5f;
    public float health = 100f;
    public float maxHealth = 100f;
    public int coinCount = 0;
    public int keyCount = 0;
    public bool invincibilityFrame = false;
    GameObject healthBar;
    SpriteRenderer sR;
    SpriteRenderer weapon;

    public Sprite[] playerSprites = new Sprite[4];


    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        weapon = GameObject.Find("Weapon").GetComponent<SpriteRenderer>();
        healthBar = GameObject.Find("HealthBar");
    }

    void FixedUpdate()
    {
        //walks if not taking knockback
        if (!knockback)
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //player turner by input and weapon layer
        if (Input.GetKeyDown(KeyCode.D))
        {
            TurnPlayer(0);
            weapon.sortingOrder = 1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            TurnPlayer(1);
            weapon.sortingOrder = 1;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            TurnPlayer(2);
            weapon.sortingOrder = 0;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            TurnPlayer(3);
            weapon.sortingOrder = 1;
        }

        if (health <= 0)
        {
            sR.enabled = true;
            Time.timeScale = 0;
            // TODO: ADD DEATH SCENE \\ 
            //weapon auto deactivates on death, drop weapon anim?
        }
    }

    void TurnPlayer(int direction)
    {
        sR.sprite = playerSprites[direction];
    }

    public void ContactDMG(float dmg, Vector3 dir)
    {
        //if not invincible, knockback with direction of hit and take damage
        if(!invincibilityFrame)
            StartCoroutine(Knockback(dir));
        StartCoroutine(TakeDamage(dmg));
    }

    public IEnumerator TakeDamage(float damage)
    {
        //if not invincible, take damage, start invincibility animation and update healthbar
        if (!invincibilityFrame)
        {
            invincibilityFrame = true;
            StartCoroutine(InvincibilityFrameAnim());
            health -= damage;
            healthBar.GetComponent<HealthBar>().SetHealth(health);
            yield return new WaitForSeconds(0.5f);
            invincibilityFrame = false;
        }
    }

    public IEnumerator Knockback(Vector3 hitDir)
    {
        knockback = true;
        body.AddForce(hitDir * 400f);
        yield return new WaitForSeconds(0.2f);
        knockback = false;
    }

    IEnumerator InvincibilityFrameAnim()
    {
        //player flickers when hit
        for (int i = 0; i < 5; i++)
        {
            sR.enabled = false;
            weapon.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sR.enabled = true;
            weapon.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }
}