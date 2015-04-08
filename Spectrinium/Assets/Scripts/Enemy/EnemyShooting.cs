using UnityEngine;
using System.Collections;

// controls enemy shooting
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

    private Animator anim;
    private bool shooting = false;
    

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

        if ((shot > 0.5f)&&(!shooting))
        {
            shooting = true;
            FireBullet();
        }
        if (shot <= 0.5f)
            shooting = false;
    }

    public void StopShooting()
    {
        anim.SetBool(Animator.StringToHash("PlayerInSight"), false);
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
