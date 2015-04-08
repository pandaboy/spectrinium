using UnityEngine;
using System.Collections;

public class EnemyBulletRays : MonoBehaviour
{
    public Transform spawn;
    public GameObject bulletPrefab;
    public GameObject muzzleFlash;

    public float spread = 1;
    public float speed = 50;
    public float damage = 10;
    public float fireRate = 2;
    public float fireRange = 5;
    private float nextFireTime = 0.0f;

    private EnemyAI enemySelf;
    private PerFrameRaycast raycast;
    private Animator anim;

    private bool shooting = false;

    void Awake()
    {
        muzzleFlash.SetActive(false);
        raycast = gameObject.AddComponent<PerFrameRaycast>();
    }

    void Start()
    {
        enemySelf = GetComponentInParent<EnemyAI>();
        anim = GetComponentInParent<Animator>();
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

        if ((shot > 0.5f) && (!shooting))
        {
            shooting = true;
            OnStartFire();
            FireBullet();
        }
        else
            muzzleFlash.SetActive(false);

        if (shot < 0.5f)
            shooting = false;

        
    }

    public void StopShooting()
    {
        anim.SetBool(Animator.StringToHash("PlayerInSight"), false);
    }

    void FireBullet()
    {
        GameObject bulletInstance = (GameObject)Instantiate(bulletPrefab, spawn.position, spawn.rotation);
        EnemyBullet bullet = bulletInstance.GetComponent<EnemyBullet>();
        bullet.damage = damage;
        bullet.speed = speed;
        bullet.owner = enemySelf;

        // fire a ray into the scene
        RaycastHit hit = raycast.GetInfo();
        if (hit.transform)
        {
            if (hit.collider) {
                Collider other = hit.collider;

                if (!other.isTrigger)
                {
                    GameObject otherObject = other.gameObject;
                    // check if the other object is a player/enemy - hurt it if it is
                    if (otherObject.CompareTag("Player"))
                    {
                        PlayerResources player = otherObject.GetComponentInParent<PlayerResources>();
                        player.Shot(damage);
                    }

                    // check to make sure that the object is not just the enemy who shot it
                    EnemyAI enemyHit = otherObject.GetComponentInParent<EnemyAI>();

                    if ((enemyHit == null) || (enemyHit != enemySelf))
                    {
                        //Destroy(gameObject);
                    }
                }
            }
        }
    }

    void OnStartFire()
    {
        if (Time.timeScale == 0) return;
        muzzleFlash.SetActive(true);
        GetComponent<AudioSource>().Play();
    }
}
