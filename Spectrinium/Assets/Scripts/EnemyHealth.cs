using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	public int max_health = 100;
	public int current_health;


	void Start()
	{
		current_health = max_health;
	}


	public void shot(int dec)
	{
		current_health -= dec;

		if(current_health < 0)
			current_health = 0;
	}
}
