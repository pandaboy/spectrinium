using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public EnemySight sight;
    public EnemyHear hearing;
    private NavMeshAgent nav;

    public float runSpeed = 2f;
    public float walkSpeed = 1f;

    public float waitTime = 1f;
    private float chaseTimer;


    void Start()
    {
  
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (sight.playerInSight)
            Chase();
        else if (hearing.playerHeard)
            Look();
        else
            Idle();
    }

    void Attack()
    {
        
    }

    void Chase()
    {
        Debug.Log("chasing");

        Vector3 lastSeen = sight.lastSeenPosition;
        Vector3 diffSighting = lastSeen - transform.position;

        if (diffSighting.sqrMagnitude >= runSpeed)
            nav.destination = lastSeen;

        nav.speed = runSpeed;
		/*
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            chaseTimer = Time.deltaTime;

            if (chaseTimer > waitTime)
            {
                chaseTimer = 0f;
            }
        }
        else
            chaseTimer = 0f;
*/
    }

    void Look()
    {
        Debug.Log("looking");

        Vector3 lastHeard = hearing.lastHeardPosition;
		Vector3 diffHearing = lastHeard - transform.position;

		if (diffHearing.sqrMagnitude >= walkSpeed)
			nav.destination = lastHeard;

        nav.speed = walkSpeed;
/*
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            chaseTimer = Time.deltaTime;

            if (chaseTimer > waitTime)
            {
                chaseTimer = 0f;
            }
        }
        else
            chaseTimer = 0f;
*/
    }

    void Idle()
    {
        Debug.Log("idle");
        nav.Stop();
    }
}
