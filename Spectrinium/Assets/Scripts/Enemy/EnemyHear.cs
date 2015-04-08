using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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



    private EnemyPathfinder pathFinder;

    void Awake()
    {
        playerHeard = false;
        col = GetComponent<SphereCollider>();


  
        enemy = enemyObject.GetComponent<EnemyAI>();


        pathFinder = enemyObject.GetComponent<EnemyPathfinder>();
    }

    //rigidbody in hear sphere
    void OnTriggerStay(Collider other)
    {
        GameObject other_object = other.gameObject;


        //if the object is the player and they are in the same wavelength
        if (other_object.tag == "Player")
        {
            string waveStr;

            waveStr = enemy.wavelength;


            if (other_object.layer == LayerMask.NameToLayer(waveStr))
            {


                playerHeard = true;
                //           Debug.Log("i can hear the player");
                lastHeardPosition = other_object.transform.position;
       
 
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




    public void UpdateHearingRange(float newRange)
    {
        col.radius = newRange * scaleFactor;
    }
}
