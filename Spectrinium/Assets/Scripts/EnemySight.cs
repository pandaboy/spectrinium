using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
	public GameObject eyes;

    void OnTriggerStay(Collider other)
    {
		Debug.Log ("sense something");
        GameObject other_object = other.gameObject;

        if (other_object.tag == "Player")
		{
            Debug.Log("sense player");

			Vector3 player_position = other_object.transform.position;
			Vector3 eye_position = eyes.transform.position;
			Vector3 dir = player_position - eye_position;
			RaycastHit hitInfo;

			bool hit = Physics.Raycast(eye_position, dir.normalized, out hitInfo);

			if(hit)
			{
				Debug.Log ("i can see something");
				Collider coll = hitInfo.collider;
				if(coll.gameObject.tag == "Player")
					Debug.Log ("i can see the player");
			}
		}
    }
}
