using UnityEngine;
using System.Collections;

public class BulletRays : MonoBehaviour {

    public Transform spawn;
    public GameObject bulletPrefab;
    public GameObject muzzleFlash;
    public float spread = 1;
    public float life = 0.5F;
    public float dist = 10000;
    public float speed = 50;
    public float forcePerSecond = 20;
    public int damage = 10;

    private bool firing = false;
    private PlayerResources player;
    private PerFrameRaycast raycast;
    private float lastFireTime = -1;

    void Awake()
    {
        muzzleFlash.SetActive(false);
        raycast = gameObject.AddComponent<PerFrameRaycast>();
    }

	void Start () {
        player = gameObject.GetComponentInParent<PlayerResources>();
	}

    void OnStartFire()
    {
        if (Time.timeScale == 0) return;
        firing = true;
        muzzleFlash.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    void OnStopFire()
    {
        muzzleFlash.SetActive(false);
        firing = false;
    }
	
	void Update () {
        if(Input.GetButtonDown("Fire1") && player.FireSpectrinium()) {
            OnStartFire();
        }
        else {
            OnStopFire();
        }

        if (firing)
        {
            if (Time.time > lastFireTime + 1 / player.fireRate)
            {
                Quaternion coneRandomRotation = Quaternion.Euler(Random.Range(-spread, spread), Random.Range(-spread, spread), 0);
			    GameObject go = (GameObject)Instantiate(bulletPrefab, spawn.position, spawn.rotation * coneRandomRotation);
			    SimpleBullet bullet = go.GetComponent<SimpleBullet>();

                lastFireTime = Time.time;

                // fire the bullet
                RaycastHit hit = raycast.GetInfo();
                if(hit.transform) {
                    if (hit.collider)
                    {
                        GameObject other = hit.collider.gameObject;
                        if (other.tag == "Environment")
                        {
                            //Debug.Log("Connected with environment!");
                        }

                        if (other.GetComponent<EnemyHealth>() != null)
                        {
                            other.GetComponent<EnemyHealth>().shot(damage);
                        }
                    }

                    // apply a force to the rigid body hit
                    if (hit.rigidbody)
                    {
                        Vector3 force = transform.forward * (forcePerSecond / player.fireRate);
                        hit.rigidbody.AddForceAtPosition(force, hit.point, ForceMode.Impulse);
                    }

                    bullet.dist = hit.distance;
                } else {
                    bullet.dist = 1000;
                }
            }
        }
	}
}
