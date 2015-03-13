using UnityEngine;
using System.Collections;

//controls enemy shooting
public class EnemyShooting : MonoBehaviour
{

    public Transform bulletSpawnPos;
    public GameObject enemyBulletPrefab;
    public float fireRate;
    public float fireRange;
    public float bulletSpeed;

    private float nextFireTime = 0.0f;
    public float damage;

    private EnemyAI enemySelf;
    

    void Start()
    {
        enemySelf = GetComponentInParent<EnemyAI>();
    }

    public void Shoot()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            FireBullet();
        }
    }

    void FireBullet()
    {
        GameObject bulletInstance = (GameObject)Instantiate(enemyBulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
        EnemyBullet bullet = bulletInstance.GetComponent<EnemyBullet>();
        bullet.damage = damage;
        bullet.speed = bulletSpeed;
        bullet.owner = enemySelf;
    }

}
