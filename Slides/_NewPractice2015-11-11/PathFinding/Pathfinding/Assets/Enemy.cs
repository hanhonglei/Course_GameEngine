using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
    public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
    public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
    public Transform patrolWayPoints;                     // An array of transforms for the patrol route.
    public float shootRotSpeed = 5f;                        // 瞄准时候旋转朝向的速度
    public float sqrPlayerDist = 4f;

    private EnemySight enemySight;                          // Reference to the EnemySight script.
    private UnityEngine.AI.NavMeshAgent nav;                               // Reference to the nav mesh agent.
    private float chaseTimer;                               // A timer for the chaseWaitTime.
    private float patrolTimer;                              // A timer for the patrolWaitTime.
    private int wayPointIndex;                              // A counter for the way point array.
    private bool chase = false;                                     // 当遇到攻击或者在射击的时候玩家跑开的话
    private Transform player;                               // Reference to the player's transform.

    public Rigidbody bullet;
    public float shootFreeTime = 2f;
    private float shootTimer = 0f;

    void Awake()
    {
        // Setting up the references.
        enemySight = transform.Find("EnemySight").GetComponent<EnemySight>();
        Debug.Assert(enemySight);
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }


    void Update()
    {
        //Debug.Log("Update!");
        // If the player is in sight and is alive...
        if (enemySight.playerInSight)
        // ... shoot.
        {
            Shooting();
            chase = true;
        }

        // If the player has been sighted and isn't dead...
        else if (chase)
            // ... chase.
            Chasing();

        // Otherwise...
        else
        // ... patrol.
        {
            Patrolling();
            //Debug.Log("Patrol!");
        }
    }


    void Shooting()
    {
        Vector3 lookPos = player.position;
        lookPos.y = transform.position.y;

        Vector3 targetDir = lookPos - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), Mathf.Min(1F, Time.deltaTime * shootRotSpeed));

        // Stop the enemy where it is.
        nav.isStopped = true;

        if(Vector3.Angle(transform.forward, targetDir) < 2)
        {
            if(shootTimer > shootFreeTime)
            {
               Instantiate(bullet, transform.position, Quaternion.LookRotation(player.position - transform.position));
                shootTimer = 0f;

            }
            shootTimer += Time.deltaTime;
        }
    }


    void Chasing()
    {
        //Debug.Log("Chasing");
        nav.isStopped = false;
        // Create a vector from the enemy to the last sighting of the player.
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;

        // If the the last personal sighting of the player is not close...
        if (sightingDeltaPos.sqrMagnitude > sqrPlayerDist)
            // ... set the destination for the NavMeshAgent to the last personal sighting of the player.
            nav.destination = enemySight.personalLastSighting;

        // Set the appropriate speed for the NavMeshAgent.
        nav.speed = chaseSpeed;

        // If near the last personal sighting...
        if (nav.remainingDistance < nav.stoppingDistance)
        {
            // ... increment the timer.
            chaseTimer += Time.deltaTime;

            // If the timer exceeds the wait time...
            if (chaseTimer >= chaseWaitTime)
            {
                // ... reset last global sighting, the last personal sighting and the timer.
                //lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                chase = false;
                chaseTimer = 0f;
            }
        }
        else
            // If not near the last sighting personal sighting of the player, reset the timer.
            chaseTimer = 0f;
    }


    void Patrolling()
    {
        nav.isStopped = false;
        // Set an appropriate speed for the NavMeshAgent.
        nav.speed = patrolSpeed;

        // If near the next waypoint or there is no destination...
        if (nav.remainingDistance < nav.stoppingDistance)
        {
            // ... increment the timer.
            patrolTimer += Time.deltaTime;

            // If the timer exceeds the wait time...
            if (patrolTimer >= patrolWaitTime)
            {
                // ... increment the wayPointIndex.
                if (wayPointIndex == patrolWayPoints.childCount - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;

                // Reset the timer.
                patrolTimer = 0;
            }
        }
        else
            // If not near a destination, reset the timer.
            patrolTimer = 0;

        // Set the destination to the patrolWayPoint.
        nav.destination = patrolWayPoints.GetChild(wayPointIndex).position;
    }
}
