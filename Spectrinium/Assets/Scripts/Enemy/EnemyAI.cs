using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

//controls enemy fsm
public class EnemyAI : MonoBehaviour
{
    public EnemySight sight;
    public EnemyHear hearing;
    private NavMeshAgent nav;
    public EnemyShooting gun;

    public GameObject floor_group;
    private List<GameObject> floor_objects;
    private int num_floorObjects = 0;

    public float runSpeed = 2f;
    public float walkSpeed = 1f;
    public float turnSpeed = 2f;

    public int numWayPoints = 2;
    public float patrolSpeed = 0.5f;
    public Vector3[] patrolWayPoints;
    public float patrolWaitTime = 1.0f;
    private float patrolTimer;
    private int wayPointIndex = 0;
    private bool wayPointsSet = false;
    

    public float waitTime = 1f;
    private float chaseTimer;


	private Vector3 lastSeen;
	private float sq_distance;

    public string wavelength;

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(wavelength);

        
    }
    /*
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            gun.Shoot();

        if (sight.playerInSight)
            if (checkInRange())
                Attack();
            else
                Chase();
        else if (hearing.playerHeard)
            Look();
        else
            if (wayPointsSet)
                Patrol();
            else
                Idle();
    }
    */

    void Update()
    {
        Patrol();
    }

    //turns towards player and shoots
    void Attack()
    {
        Debug.Log("pew pew");
		nav.Stop ();
        Vector3 dist_vec = lastSeen - GetComponent<Rigidbody>().position;
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
/*
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(transform.position, out closestHit, 500, 1))
        {
            transform.position = closestHit.position;
            nav = gameObject.AddComponent<NavMeshAgent>();
        }
        else
        {
             Debug.Log("oh no");
        }
 * */

        nav = gameObject.AddComponent<NavMeshAgent>();
        nav = GetComponent<NavMeshAgent>();
        nav.stoppingDistance = 0.8f;

        SetNavLayer(nav);

        hearing.SetNavMeshAgent();

        SetPatrolWayPoints();
    }

    private void SetNavLayer(NavMeshAgent nma)
    {
        int d = NavMesh.GetNavMeshLayerFromName("Default");
        int space = NavMesh.GetNavMeshLayerFromName("Space");
        int R = NavMesh.GetNavMeshLayerFromName("Red");
        int G = NavMesh.GetNavMeshLayerFromName("Green");
        int B = NavMesh.GetNavMeshLayerFromName("Blue");
        int RG = NavMesh.GetNavMeshLayerFromName("RedGreen");
        int RB = NavMesh.GetNavMeshLayerFromName("RedBlue");   
        int GB = NavMesh.GetNavMeshLayerFromName("GreenBlue");
        int RGB = NavMesh.GetNavMeshLayerFromName("RedGreenBlue");

        int layerMask = (1 << d) + (1 << space);

        if (wavelength == "Red")
            layerMask += (1 << G) + (1 << B) + (1 << GB);
        else if (wavelength == "Green")
            layerMask += (1 << R) + (1 << B) + (1 << RB);
        else
            layerMask += (1 << R) + (1 << G) + (1 << RG);

        nma.walkableMask = layerMask;




       
    }


    void Patrol()
    {
        nav.speed = patrolSpeed;

        if (nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                if (wayPointIndex >= patrolWayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;

                patrolTimer = 0.0f;
            }
          
        }
        else
            patrolTimer = 0.0f;

        nav.destination = patrolWayPoints[wayPointIndex];

    }

    void SetPatrolWayPoints()
    {
        patrolWayPoints = new Vector3[numWayPoints];

        GameObject[] environmentObjects = GameObject.FindGameObjectsWithTag("Environment");
        int numEnvironmentObjects = environmentObjects.Length;

        int count = 0;

        while (count < numWayPoints)
        {
  

            while (true)
            {
                int objectNum = Random.Range(0, numEnvironmentObjects);

                GameObject tile = environmentObjects[objectNum];

                if (tile.name == "Floor")
                {
                    int tileLayerID = GameObjectUtility.GetNavMeshLayer(tile);

         
                    string tileLayerString = GameObjectUtility.GetNavMeshLayerNames()[tileLayerID];
                    int layerMask = nav.walkableMask;

                    int check = layerMask >> tileLayerID;
                    if(check%2 != 0)
                    {
                        patrolWayPoints[count] = tile.transform.position;
                        count++;
                        break;
                    }

                }


            }
        }
        wayPointsSet = true;
        
    }

    private void AssignFloorObjects()
    {
        floor_objects = new List<GameObject>();
        foreach (Transform child in floor_group.transform)
        {
            floor_objects.Add(child.gameObject);
            num_floorObjects++;
        }
    }



    public Vector3 FindRandomClearPosition()
    {
        for (int i = 0; i < 10; i++)
        {
            int randomNum = Random.Range(0, num_floorObjects - 1);

            GameObject floorObject = floor_objects[randomNum];


            int tileLayerID = GameObjectUtility.GetNavMeshLayer(floorObject);


            string tileLayerString = GameObjectUtility.GetNavMeshLayerNames()[tileLayerID];
            int layerMask = nav.walkableMask;

            int check = layerMask >> tileLayerID;
            if (check % 2 != 0)
            {
                return floorObject.transform.position;
            }
        }
        
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void AssignFloors(GameObject floors)
    {
        floor_group = floors;

        AssignFloorObjects();
    }
}
