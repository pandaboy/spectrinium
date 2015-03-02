using UnityEngine;
using System.Collections;

//controls enemy fsm
public class EnemyAI : MonoBehaviour
{
    public EnemySight sight;
    public EnemyHear hearing;
    private NavMeshAgent nav;
    public EnemyShooting gun;

    public float runSpeed = 2f;
    public float walkSpeed = 1f;
    public float turnSpeed = 2f;

    public float waitTime = 1f;
    private float chaseTimer;


	private Vector3 lastSeen;
	private float sq_distance;

    public string wavelength;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(wavelength);
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

    //turns towards player and shoots
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

    //clamps an angle to between 0-360
    float zeroTo360Range(float angle)
    {
        float new_angle = angle;

        while (new_angle < 0)
            new_angle += 360;

        while (new_angle >= 360)
            new_angle -= 360;

        return new_angle;
    }

    //checks if the enemy is within firing range of the player
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

    //runs towards players position
    void Chase()
    {
        Debug.Log("chasing");


        if (sq_distance >= runSpeed)
            nav.destination = lastSeen;

        nav.speed = runSpeed;
    }

    //walks towards players position
    void Look()
    {
        Debug.Log("looking");

        Vector3 lastHeard = hearing.lastHeardPosition;
		Vector3 diffHearing = lastHeard - transform.position;

		if (diffHearing.sqrMagnitude >= walkSpeed)
			nav.destination = lastHeard;

        nav.speed = walkSpeed;
    }

    //does nothing
    void Idle()
    {
        Debug.Log("idle");
        nav.Stop();
    }

    public void SetupNavMeshAgent()
    {

        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(transform.position, out closestHit, 500, 1))
        {
            transform.position = closestHit.position;
            gameObject.AddComponent<NavMeshAgent>();
            //TODO
            nav = GetComponent<NavMeshAgent>();
        }
        else
        {
            Debug.Log("oh no");
        }


        SetNavLayer(nav);

        hearing.SetNavMeshAgent();
    }

    private void SetNavLayer(NavMeshAgent nma)
    {
        if (wavelength == "Red")
            nma.walkableMask = 1433;
        else if (wavelength == "Green")
            nma.walkableMask = 1705;
        else
            nma.walkableMask = 1865;
    }
}
