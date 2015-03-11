using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {


	public Transform spawnPoint;
	public GameObject bulletPrefab;
    public float fireRate;
    private PlayerResources player;

    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerResources>();
    }

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("Fire1"))
        {
            if (player.FireSpectrinium())
            {
                GameObject bulletInstance = (GameObject)Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                GetComponent<AudioSource>().Play();
            }
		}
	}
}
