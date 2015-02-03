using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {

	public Transform bullet;
	public int forwardForce = 1000;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			Transform bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity) as Transform;
			bulletInstance.rigidbody.AddForce(transform.forward * forwardForce);
		}
	}
}
