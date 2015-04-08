using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    public EnemyAI owner;
	// lifetime of the bullet
	public float lifetime = 3.0f;

	// damage the bullet does if it hits an active object
	public float damage;

	// bullet speed
	public float speed;
	
	// update the bullet
	void FixedUpdate ()
    {
		lifetime -= Time.deltaTime;
		
		if(lifetime <= 0.0f) {
			Destroy(gameObject);
		}
		
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
	
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
