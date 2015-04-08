using UnityEngine;
using System.Collections;

public class EnemySimpleBullet : SimpleBullet
{
    public EnemyAI owner;

    // damage the bullet does if it hits an active object
    public float damage;

    void OnTriggerStay(Collider other)
    {
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

            if ((enemyHit == null) || (enemyHit != owner))
            {
                Destroy(gameObject);
            }
        }
    }

}