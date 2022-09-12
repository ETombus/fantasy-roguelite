using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMoveAI : MonoBehaviour
{
    public bool rangedShooter = false;
    public float shooterDist;
    public float speed = 200f;
    public float nextWaypointDist = 0.1f;

    Path path;
    float range;
    float agroRange = 10f;
    int layerMask = 1 << 2;
    int currentWaypoint = 0;
    bool reachedEndOFPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    Transform player;
    RaycastHit2D visual;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        layerMask = ~layerMask;

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        //tries to grab range value from enemy, if enemy can shoot
        try
        { range = GetComponent<EnemyShootScript>().range; }
        catch { return; }
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, player.position, OnPathComplete);
    }

    void FixedUpdate()
    {
        visual = Physics2D.Raycast(transform.position, player.position - transform.position, 10f, layerMask);

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOFPath = true;
            return;
        }
        else
            reachedEndOFPath = false;

        float dist = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (rangedShooter && dist <= shooterDist && visual.collider != null && visual.collider.gameObject.tag == "Player" 
            && Vector3.Distance(player.position, transform.position) < range)
        {
            return;
        }
        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;

        if (Vector3.Distance(player.position, transform.position) < agroRange && visual.collider != null && visual.collider.gameObject.tag == "Player")
            rb.AddForce(force);

        if ((dist < nextWaypointDist && currentWaypoint++ < path.vectorPath.Count) || currentWaypoint <= 0)
        {
            currentWaypoint++;
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
