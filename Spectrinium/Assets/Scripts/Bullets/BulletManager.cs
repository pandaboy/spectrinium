using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour
{
	public Transform spawnPoint;
	public GameObject bulletPrefab;
    public float fireRate;

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("Fire1"))
        {
            GameObject bulletInstance = (GameObject)Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            GetComponent<AudioSource>().Play();
		}
	}
}
