using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
	public GameObject eyes;
    public bool playerInSight;

    public Vector3 lastSeenPosition;

    void Start()
    {
        playerInSight = false;
    }



    //rigidbody in view cone
    void OnTriggerStay(Collider other)
    {


        GameObject other_object = other.gameObject;

        //if the object is the player
        if (other_object.tag == "Player")
        {
			Vector3 player_position = other_object.transform.position;
			Vector3 eye_position = eyes.transform.position;
			Vector3 dir = player_position - eye_position;
			RaycastHit hitInfo;

			bool hit = Physics.Raycast(eye_position, dir.normalized, out hitInfo);

            //if view unobstructed
			if(hit)
			{
				Collider coll = hitInfo.collider;
                if (coll.gameObject.tag == "Player")
                {
                    playerInSight = true;
                    Debug.Log("i can see the player");
                    lastSeenPosition = coll.gameObject.transform.position;
                }
			}
		}
    }

    void OnTriggerExit(Collider other)
    {
        GameObject other_object = other.gameObject;

        if(other_object.tag == "Player")
            playerInSight = false;
    }
}
