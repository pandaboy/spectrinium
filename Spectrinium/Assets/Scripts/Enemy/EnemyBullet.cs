using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{

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
		// check if the other object is a player/enemy - hurt it if it is
		if(other.gameObject.tag == "Player")
        {
            /*
			TO DO ---- IMPLEMENT GET HURT FUNCTION IN PLAYER
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            player.shot(damage);
            */
		}
		
		// kill the bullet as soon as we hit the other object
		if(other.gameObject.tag == "Environment" || other.gameObject.tag == "Player")
        {
			Destroy(gameObject);
		}
	}
}
