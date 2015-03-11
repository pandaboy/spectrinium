using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	// lifetime of the bullet
	public float lifetime = 3.0f;

	// damage the bullet does if it hits an active object
	public int damage = 10;

	// bullet speed
	public int speed = 100;

	// update the bullet
	void FixedUpdate ()
    {
		lifetime -= Time.deltaTime;
		
		if(lifetime <= 0.0f) {
			Destroy(gameObject);
		}
		
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
	
	void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);

		// check if the other object is a player/enemy - hurt it if it is
		if(other.gameObject.tag == "Enemy")
        {
			// deduct it's health if it has the EnemyHealth component
			if(other.gameObject.GetComponent<EnemyHealth>() != null)
            {
				other.gameObject.GetComponent<EnemyHealth>().shot(damage);
			}
		}
		
		// kill the bullet as soon as we hit the other object
		if(other.gameObject.tag == "Environment" || other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit something");
			Destroy(gameObject);
		}
	}
}
