using UnityEngine;
using System.Collections;

public class EnemyBulletRays : MonoBehaviour
{
    public Transform spawn;
    public GameObject bulletPrefab;
    public GameObject muzzleFlash;

    public float spread = 1;
    public float life = 0.5F;
    public float dist = 10000;
    public float speed = 50;
    public float forcePerSecond = 20;
    public float damage = 10;
    public float rate;
    public float range;

    private bool firing = false;
    private float lastFireTime = -1;
    private float nextFireTime = 0.0f;

    private EnemyAI enemySelf;
    private PerFrameRaycast raycast;
    private Animator anim;

    void Start()
    {
        enemySelf = GetComponentInParent<EnemyAI>();
        anim = GetComponentInParent<Animator>();
    }

    void Awake()
    {
        muzzleFlash.SetActive(false);
        raycast = gameObject.AddComponent<PerFrameRaycast>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        float aimWeight = anim.GetFloat(Animator.StringToHash("AimWeight"));
        anim.SetIKPosition(AvatarIKGoal.RightHand, enemySelf.lastSeen + Vector3.up * 1.5f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
    }

    public void Shoot()
    {
        anim.SetBool(Animator.StringToHash("PlayerInSight"), true);

        float shot = anim.GetFloat(Animator.StringToHash("Shot"));

        if ((shot > 0.5f) && (Time.time > nextFireTime))
        {
            nextFireTime = Time.time + rate;
            OnStartFire();
            FireBullet();
        }
        else
        {
            OnStopFire();
        }
    }

    void FireBullet()
    {
        /*
        GameObject bulletInstance = (GameObject)Instantiate(enemyBulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
        EnemyBullet bullet = bulletInstance.GetComponent<EnemyBullet>();
        bullet.damage = damage;
        bullet.speed = bulletSpeed;
        bullet.owner = enemySelf;
        */

        Quaternion coneRandomRotation = Quaternion.Euler(Random.Range(-spread, spread), Random.Range(-spread, spread), 0);
        GameObject go = (GameObject)Instantiate(bulletPrefab, spawn.position, spawn.rotation * coneRandomRotation);
        EnemySimpleBullet bullet = go.GetComponent<EnemySimpleBullet>();
        bullet.damage = damage;
        bullet.speed = speed;
        bullet.owner = enemySelf;

        // fire a ray into the scene
        RaycastHit hit = raycast.GetInfo();
        if (hit.transform)
        {
            if (hit.collider) {
                GameObject other = hit.collider.gameObject;
            }

            bullet.dist = hit.distance;
        }
        else
        {
            bullet.dist = 1000;
        }
    }

    void OnStartFire()
    {
        //if (Time.timeScale == 0) return;
        muzzleFlash.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    void OnStopFire()
    {
        muzzleFlash.SetActive(false);
        firing = false;
    }

    /*
    void Update()
    {
        if (firing)
        {
            if (Time.time > lastFireTime + 1 / rate)
            {
                Quaternion coneRandomRotation = Quaternion.Euler(Random.Range(-spread, spread), Random.Range(-spread, spread), 0);
                GameObject go = (GameObject)Instantiate(bulletPrefab, spawn.position, spawn.rotation * coneRandomRotation);
                SimpleBullet bullet = go.GetComponent<SimpleBullet>();

                lastFireTime = Time.time;

                // fire the bullet
                RaycastHit hit = raycast.GetInfo();
                if (hit.transform)
                {
                    if (hit.collider)
                    {
                        GameObject other = hit.collider.gameObject;
                    }

                    // apply a force to the rigid body hit
                    if (hit.rigidbody)
                    {
                        Vector3 force = transform.forward * (forcePerSecond / rate);
                        hit.rigidbody.AddForceAtPosition(force, hit.point, ForceMode.Impulse);
                    }

                    bullet.dist = hit.distance;
                }
                else
                {
                    bullet.dist = 1000;
                }
            }
        }
    }
    */
}
