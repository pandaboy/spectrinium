using UnityEngine;
using System.Collections;

public class SimpleBullet : MonoBehaviour {

    public float speed = 10;
    public float lifeTime = 0.5F;
    public float dist = 10000;

    private float spawnTime = 0;

	// Use this for initialization
	void Start () {
        spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * speed;
        dist -= speed * Time.deltaTime;

        // kill self after life time or if distance is small
        if (Time.time > spawnTime + lifeTime || dist < 0)
        {
            Destroy(gameObject);
        }
	}
}
