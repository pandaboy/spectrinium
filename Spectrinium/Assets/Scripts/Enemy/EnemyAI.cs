using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.Collections.Generic;

//controls enemy fsm
public class EnemyAI : MonoBehaviour
{
    public EnemySight sight;
    public EnemyHear hearing;
    private NavMeshAgent nav;
  //  public EnemyShooting gun;
    public EnemyBulletRays gun;

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


	public Vector3 lastSeen;
	private float sq_distance;

    public string wavelength;

    private int navMask;
    private EnemyPathfinder pathFinder;

    public bool playerInRange = false;

    private EnemyHealth enemyHealth;

 

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(wavelength);

        pathFinder = gameObject.GetComponent<EnemyPathfinder>();
        enemyHealth = gameObject.GetComponent<EnemyHealth>();
    }
    
    void Update()
    {
        if(!enemyHealth.dead)
        {
            if (sight.playerInSight)
                if (checkInRange())
                {
                    playerInRange = true;
                    Attack();
                }
                else
                {
                    playerInRange = false;
                    Chase();
                }
            else
            {
                playerInRange = false;

                if (hearing.playerHeard)
                    Look();
                else
                    if (wayPointsSet)
                        Patrol();
                    else
                        Idle();
            }
        }
        else
            playerInRange = false;

        if (!playerInRange)
        {
            gun.StopShooting();
        }
    }
    
    /*
    void Update()
    {
        Patrol();
    }
    */
    //turns towards player and shoots
    void Attack()
    {
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
        nav.Resume();
//        Debug.Log("chasing");


        if (sq_distance >= runSpeed)
            nav.destination = lastSeen;

        nav.speed = runSpeed;
    }

    //walks towards players position
    void Look()
    {
        nav.Resume();
       // Debug.Log("looking");

        Vector3 lastHeard = hearing.lastHeardPosition;
		Vector3 diffHearing = lastHeard - transform.position;

        if (diffHearing.sqrMagnitude >= walkSpeed)
            nav.SetDestination(lastHeard);

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

        nav = gameObject.AddComponent<NavMeshAgent>();
        nav = GetComponent<NavMeshAgent>();
        nav.stoppingDistance = 1f;
        nav.acceleration = 3f;

        SetNavLayer(nav);

        gameObject.GetComponent<EnemyAnimation>().SetupNavMeshAgent();

        SetPatrolWayPoints();
    }

    private void SetNavLayer(NavMeshAgent nma)
    {

        int d = NavAreas.Walkable;
        int space = NavAreas.Space;
        int R = NavAreas.Red;
        int G = NavAreas.Green;
        int B = NavAreas.Blue;
        int RG = NavAreas.RedGreen;
        int RB = NavAreas.RedBlue;
        int GB = NavAreas.GreenBlue;
        int RGB = NavAreas.RedGreenBlue;

        int layerMask = (1 << d) + (1 << space);

        if (wavelength == "Red")
            layerMask += (1 << G) + (1 << B) + (1 << GB);
        else if (wavelength == "Green")
            layerMask += (1 << R) + (1 << B) + (1 << RB);
        else
            layerMask += (1 << R) + (1 << G) + (1 << RG);

        navMask = layerMask;


       
    }


    void Patrol()
    {
        nav.Resume();
        nav.speed = patrolSpeed;

        if (nav.remainingDistance < nav.stoppingDistance)
        {
            Vector2 currTile = Map.getClosestTileCoord(gameObject.transform.position);
            Vector3 currPatrolPoint = patrolWayPoints[wayPointIndex];
            Vector2 goalTile = Map.getClosestTileCoord(currPatrolPoint);
            Tile tile = Map.tiles[(int)currTile.x, (int)currTile.y];
            Vector2 nextTile = new Vector2();
            pathFinder.FindPath(currTile, goalTile, ref nextTile);

            if (currTile == goalTile)
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
            {
                Tile nextWorldTile = Map.tiles[(int)nextTile.x, (int)nextTile.y];
                nav.destination = nextWorldTile.worldPos;
                patrolTimer = 0.0f;
            }
        }
        else
            patrolTimer = 0.0f;

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
                int objectNum = Random.Range(0, num_floorObjects);

                GameObject tile = floor_objects[objectNum];
                TileArea t = tile.GetComponent<TileArea>();

  
                int tileLayerID = t.navArea;


                int check = navMask >> tileLayerID;
                if (check % 2 != 0)
                {
                    patrolWayPoints[count] = tile.transform.position;
                    count++;
                    break;
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
        Vector3 originVec = new Vector3(0f, 0f, 0f);
        while (true)
        {
            int randomNum = Random.Range(0, num_floorObjects);

            Vector3 pos = GetFloorPosAtInt(randomNum);
            if (pos != originVec)
                return pos;
        }
    }

    public Vector3 FindRandomClearPosition(List<int> noSpawnList)
    {
        Vector3 originVec = new Vector3(0f, 0f, 0f);
        while (true)
        {
            int randomNum = Random.Range(0, num_floorObjects);

            if(CheckIntInList(randomNum, noSpawnList))
                continue;

            Vector3 pos = GetFloorPosAtInt(randomNum);
            if (pos != originVec)
            {
                noSpawnList.Add(randomNum);
                return pos;
            }
        }
    }

    private bool CheckIntInList(int num, List<int> list)
    {
        int lengthList = list.Count;

        for (int i = 0; i < lengthList; i++)
            if (list[i] == num)
                return true;

        return false;
    }

    private Vector3 GetFloorPosAtInt(int num)
    {
        GameObject floorObject = floor_objects[num];
        TileArea t = floorObject.GetComponent<TileArea>();

    
        int tileLayerID = t.navArea;



        int check = navMask >> tileLayerID;
        if (check % 2 != 0)
        {
            return floorObject.transform.position;
        }

        return new Vector3(0f, 0f, 0f);
    }

    public void AssignFloors(GameObject floors)
    {
        floor_group = floors;

        AssignFloorObjects();
    }
}
