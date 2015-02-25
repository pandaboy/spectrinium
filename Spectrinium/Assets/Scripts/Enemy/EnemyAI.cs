using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public EnemySight sight;
    public EnemyHear hearing;
    private NavMeshAgent nav;
    public EnemyShooting gun;

    public float runSpeed = 2f;
    public float walkSpeed = 1f;
    public float turnSpeed = 0.5f;

    public float waitTime = 1f;
    private float chaseTimer;


	private Vector3 lastSeen;
	private float sq_distance;



    void Start()
    {
  
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            gun.Shoot();

        if (sight.playerInSight)
            if(checkInRange())
				Attack();
			else
				Chase ();
        else if (hearing.playerHeard)
            Look();
        else
            Idle();
    }

    void Attack()
    {
        Debug.Log("pew pew");
		nav.Stop ();
        Vector3 dist_vec = lastSeen - rigidbody.position;
        Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
        Quaternion lookDir = Quaternion.LookRotation(dist_vec, up);

        Vector3 currEuler = transform.rotation.eulerAngles;
        float currAngle = currEuler.y;
        float wantAngle = lookDir.eulerAngles.y;

        currAngle = zeroTo360Range(currAngle);
        wantAngle = zeroTo360Range(wantAngle);

        float diffAngle = wantAngle - currAngle;

        if (Mathf.Abs(diffAngle) <= 5)
            gun.Shoot();
        else if (diffAngle < 0)
            currAngle -= turnSpeed;
        else
            currAngle += turnSpeed;

        currEuler.y = currAngle;

        transform.rotation = Quaternion.Euler(currEuler);



  //      transform.rotation = lookDir;
 //       gun.Shoot();

    }

    float zeroTo360Range(float angle)
    {
        float new_angle = angle;

        while (new_angle < 0)
            new_angle += 360;

        while (new_angle >= 360)
            new_angle -= 360;

        return new_angle;
    }


	bool checkInRange()
	{
		lastSeen = sight.lastSeenPosition;
		Vector3 dist_vec = lastSeen - transform.position;
		sq_distance = dist_vec.sqrMagnitude;

        float attackRange = gun.fireRange;

		if (sq_distance <= attackRange * attackRange)
			return true;

		return false;
	}

    void Chase()
    {
        Debug.Log("chasing");


        if (sq_distance >= runSpeed)
            nav.destination = lastSeen;

        nav.speed = runSpeed;
    }

    void Look()
    {
        Debug.Log("looking");

        Vector3 lastHeard = hearing.lastHeardPosition;
		Vector3 diffHearing = lastHeard - transform.position;

		if (diffHearing.sqrMagnitude >= walkSpeed)
			nav.destination = lastHeard;

        nav.speed = walkSpeed;
    }

    void Idle()
    {
        Debug.Log("idle");
        nav.Stop();
    }
}
