using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {

	public Transform spawnPoint;
	public Rigidbody bullet;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			Rigidbody bulletInstance = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
		}
	}
}
