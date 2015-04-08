using UnityEngine;
using System.Collections;

//controls enemy sight + view cone
public class EnemySight : MonoBehaviour
{
	public GameObject eyes;
    public bool playerInSight;
    public GameObject enemyObject;

    public Vector3 lastSeenPosition;

    private int rangeScaleFactor = 500;
    private float sightZRange;
    private float halfXRange;
    private float sightYRange;
    private float halfFOV;

    void Start()
    {
        playerInSight = false;
    }



    //rigidbody in view cone
    void OnTriggerStay(Collider other)
    {


        GameObject other_object = other.gameObject;

        //if the object is the player
        if (other_object.CompareTag("Player"))
        {
			Vector3 player_position = other_object.transform.position;
			Vector3 eye_position = eyes.transform.position;
			Vector3 dir = player_position - eye_position;
			RaycastHit hitInfo;

            int enemyLayerID = enemyObject.layer;
            string enemyLayer = LayerMask.LayerToName(enemyLayerID);

            int blueLayerID = LayerMask.NameToLayer("Blue");
            int redLayerID = LayerMask.NameToLayer("Red");
            int greenLayerID = LayerMask.NameToLayer("Green");

            int ignoreLayerID = LayerMask.NameToLayer("Ignore Raycast");

            int layerMask = 0;

            if (enemyLayer == "Red")
                layerMask = (1 << blueLayerID) + (1 << greenLayerID);
            else if (enemyLayer == "Green")
                layerMask = (1 << blueLayerID) + (1 << redLayerID);
            else
                layerMask = (1 << redLayerID) + (1 << greenLayerID);

            layerMask += 1 << ignoreLayerID;

            layerMask = ~layerMask;
            
            bool hit = Physics.Raycast(eye_position, dir.normalized, out hitInfo, 100.0f, layerMask);
           
            //if view unobstructed
			if(hit)
			{
				Collider coll = hitInfo.collider;
                if (coll.gameObject.CompareTag("Player"))
                {
                    SeesPlayer(coll.gameObject.transform.position);
                }
			}
            
		}
    }

    private void SeesPlayer(Vector3 playerPos)
    {
        playerInSight = true;
  //      Debug.Log("i can see the player");
        lastSeenPosition = playerPos;
    }

    void OnTriggerExit(Collider other)
    {
        GameObject other_object = other.gameObject;

        if(other_object.tag == "Player")
            playerInSight = false;
    }


    public void UpdateSightRange(float newRange)
    {
        sightZRange = newRange;

        float halfXRange = sightZRange * Mathf.Tan(halfFOV);
        float sightXRange = halfXRange * 2;
        sightYRange = sightXRange;

        Vector3 newScale = transform.localScale;
        newScale.x = sightXRange * rangeScaleFactor;
        newScale.y = sightYRange * rangeScaleFactor;
        newScale.z = sightZRange * rangeScaleFactor;
        transform.localScale = newScale;
    }

    public void UpdateFieldOfView(float newFOV)
    {
        halfFOV = newFOV / 2;
        halfFOV *= Mathf.Deg2Rad;

        halfXRange = sightZRange * Mathf.Tan(halfFOV);

        float sightXRange = halfXRange*2;
        sightYRange = sightXRange;

        Vector3 newScale = transform.localScale;
        newScale.x = sightXRange * rangeScaleFactor;
        newScale.y = sightYRange * rangeScaleFactor;
        transform.localScale = newScale;
    }

}
