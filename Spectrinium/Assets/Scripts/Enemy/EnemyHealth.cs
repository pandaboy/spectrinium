using UnityEngine;
using System.Collections;

//controls the enemies health and death
public class EnemyHealth : MonoBehaviour
{
	public int max_health = 100;
	public int current_health;


	void Start()
	{
		current_health = max_health;
	}

    //gets shot by bullet that does specified damage
    public void shot(int damage)
	{
        current_health -= damage;

        if (current_health <= 0)
            Destroy(gameObject);
	}
}
