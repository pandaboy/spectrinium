using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public int numEnemies;

    private GameObject floor_group;

    public GameObject enemyPrefab;

    private GameObject enemy_group;
    private GameObject red_group;
    private GameObject green_group;
    private GameObject blue_group;


	// Use this for initialization
	void Awake ()
    {
        enemy_group = new GameObject();
        enemy_group.name = "Enemies";

        red_group = new GameObject();
        red_group.name = "Red";
        red_group.transform.parent = enemy_group.transform;

        green_group = new GameObject();
        green_group.name = "Green";
        green_group.transform.parent = enemy_group.transform;

        blue_group = new GameObject();
        blue_group.name = "Blue";
        blue_group.transform.parent = enemy_group.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {	
	}

    public void UpdateVisibleCollidable()
    {
        switch (GameController.Instance.getCurrentWavelengthAsString())
        {
            case "RED":
                MakeVisibleCollidable(red_group.transform);
                ClearVisibleCollidable(green_group.transform);
                ClearVisibleCollidable(blue_group.transform);
                break;
            case "GREEN":
                MakeVisibleCollidable(green_group.transform);
                ClearVisibleCollidable(red_group.transform);
                ClearVisibleCollidable(blue_group.transform);
                break;
            case "BLUE":
                MakeVisibleCollidable(blue_group.transform);
                ClearVisibleCollidable(red_group.transform);
                ClearVisibleCollidable(green_group.transform);
                break;
        }
    }

    public void MakeVisibleCollidable(Transform wavelengthGroup)
    {
        foreach (Transform child in wavelengthGroup)
        {
            child.transform.renderer.enabled = true;
            child.transform.collider.isTrigger = false;
        }
    }


    public void ClearVisibleCollidable(Transform wavelengthGroup)
    {
        foreach (Transform child in wavelengthGroup)
        {
            child.transform.renderer.enabled = false;
            child.transform.collider.isTrigger = true;
        }
    }



    public void SpawnEnemies()
    {
        int thirdEnemies = numEnemies / 3;
        int leftOver = numEnemies - 2 * thirdEnemies;

        for (int i = 0; i < thirdEnemies; i++)
        {
            SpawnEnemy(red_group);
            SpawnEnemy(green_group);

            if (i < leftOver)
                SpawnEnemy(blue_group);
        }

       
            

    }

    private void SpawnEnemy(GameObject wavelengthGroup)
    {
        SpawnEnemy(wavelengthGroup.transform);
    }

    private void SpawnEnemy(Transform wavelengthGroup)
    {
        GameObject enemyObject = (GameObject)Instantiate(enemyPrefab);
        EnemyAI enemy = enemyObject.GetComponent<EnemyAI>();
        enemy.wavelength = wavelengthGroup.name;
        enemy.AssignFloors(floor_group);

        enemy.SetupNavMeshAgent();

        enemyObject.transform.position = enemy.FindRandomClearPosition();
        enemyObject.transform.rotation = Quaternion.Euler(Random.insideUnitSphere);

        enemyObject.transform.parent = wavelengthGroup;
    }

    public void AssignFloors(GameObject floors)
    {
        floor_group = floors;
    }

/*

    public void UpdateVisibleCollidable()
    {
        switch (GameController.Instance.getCurrentWavelengthAsString())
        {
            case "RED":
                MakeVisCol(red_group.transform);
                ClearVisCol(green_group.transform);
                ClearVisCol(blue_group.transform);
                break;
            case "GREEN":
                MakeVisCol(green_group.transform);
                ClearVisCol(red_group.transform);
                ClearVisCol(blue_group.transform);
                break;
            case "BLUE":
                MakeVisCol(blue_group.transform);
                ClearVisCol(red_group.transform);
                ClearVisCol(green_group.transform);
                break;
        }
    }

    private void ClearVisCol(Transform wavelengthGroup)
    {
        foreach (Transform child in wavelengthGroup)
        {
            child.transform.GetComponent<Renderer>().enabled = false;
            child.transform.GetComponent<Collider>().isTrigger = true;
        }
    }

    private void MakeVisCol(Transform wavelengthGroup)
    {
        foreach (Transform child in wavelengthGroup)
        {
            child.transform.GetComponent<Renderer>().enabled = true;
            child.transform.GetComponent<Collider>().isTrigger = false;
        }
    }
    */


}
