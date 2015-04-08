using UnityEngine;
using System.Collections;

//controls the enemies health and death
public class EnemyHealth : MonoBehaviour
{
	public int max_health = 100;
	public int current_health;
    Animator anim;

    public bool dead = false;
    private float deathTime = 0.0f;
    private float deathLength = 5.0f;

	void Start()
	{
		current_health = max_health;
        anim = gameObject.GetComponent<Animator>();
	}

    void FixedUpdate()
    {
        if (dead)
            if (Time.time > deathTime)
                Destroy(gameObject);
    }

    //gets shot by bullet that does specified damage
    public void shot(int damage)
	{
        if (!dead)
        {
            current_health -= damage;

            if (current_health <= 0)
                Die();
        }
	}

    private void Die()
    {
        anim.SetBool(Animator.StringToHash("PlayerInSight"), false);
        anim.SetBool(Animator.StringToHash("Dead"), true);
        dead = true;

        deathTime = Time.time + deathLength;
    }
}
