using UnityEngine;
using System.Collections;

//controls enemy hearing
public class EnemyHear : MonoBehaviour
{
    public GameObject ears;
    public bool playerHeard;
    public GameObject enemyObject;
    private EnemyAI enemy;
    private NavMeshAgent nav;
    private SphereCollider col;

    public Vector3 lastHeardPosition;

    private int scaleFactor = 10;

    void Awake()
    {
        playerHeard = false;
        col = GetComponent<SphereCollider>();
        enemyObject = ears.transform.parent.gameObject;
        enemy = enemyObject.GetComponent<EnemyAI>();
    }

    //rigidbody in hear sphere
    void OnTriggerStay(Collider other)
    {
        GameObject other_object = other.gameObject;


        //if the object is the player and they are in the same wavelength
        if (other_object.tag == "Player")
        {
            if (other_object.layer == LayerMask.NameToLayer(enemy.wavelength))
            {
                float soundPathLength = CalculateSoundPathLength(other_object.transform.position);

                if (soundPathLength < col.radius)
                {

                    playerHeard = true;
                    //           Debug.Log("i can hear the player");
                    lastHeardPosition = other_object.transform.position;
                }
            }
            else
                playerHeard = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject other_object = other.gameObject;

        //if the object is the player
        if (other_object.tag == "Player")
        {
            playerHeard = false;
        }
    }

    //calculates the path the sound takes on the navmesh - ensure player is not in another room
    float CalculateSoundPathLength(Vector3 targetPos)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
            nav.CalculatePath(targetPos, path);

        Vector3[] wayPoints = new Vector3[path.corners.Length + 2];

        wayPoints[0] = transform.position;
        wayPoints[wayPoints.Length - 1] = targetPos;

        for (int i = 0; i < path.corners.Length; i++)
            wayPoints[i + 1] = path.corners[i];

        float pathLength = 0.0f;

        for(int i=0; i<wayPoints.Length-1; i++)
        {
            float dist = Vector3.Distance(wayPoints[i], wayPoints[i+1]);
            pathLength += dist;
        }

        return pathLength;
    }


    public void SetNavMeshAgent()
    {
        nav = GetComponentInParent<NavMeshAgent>();
    }

    public void UpdateHearingRange(float newRange)
    {
        col.radius = newRange * scaleFactor;
    }
}
