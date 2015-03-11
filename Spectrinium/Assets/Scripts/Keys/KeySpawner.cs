using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeySpawner : MonoBehaviour
{


    private GameObject key_group;
    private GameObject[] keys;

    public GameObject keyPrefab;

    private List<GameObject> floor_objects;
    private int num_floorObjects;


    // Use this for initialization
    void Awake()
    {
        key_group = new GameObject();
        key_group.name = "Keys";
        keys = new GameObject[3];
    }



    public void AssignFloors(GameObject floors)
    {
        AssignFloorObjects(floors);
    }

    private void AssignFloorObjects(GameObject floor_group)
    {
        floor_objects = new List<GameObject>();
        foreach (Transform child in floor_group.transform)
        {
            floor_objects.Add(child.gameObject);
            num_floorObjects++;
        }
    }


    public void SpawnKeys()
    {
        for (int i = 0; i < 3; i++)
        {
            string wav;

            if (i == 0)
                wav = "Red";
            else if (i == 1)
                wav = "Green";
            else
                wav = "Blue";


            GameObject keyObject = (GameObject)Instantiate(keyPrefab);
            keyObject.name = wav;
            keyObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            keys[i] = keyObject;

            Key key = keyObject.GetComponent<Key>();
            key.wavelength = wav;
            Vector3 spawnPos = key.FindRandomClearPosition(floor_objects);
            spawnPos.y = keyObject.transform.position.y;
            keyObject.transform.position = spawnPos;

            keyObject.transform.parent = key_group.transform;
        }
    }




    public void UpdateVisibleCollidable()
    {
        switch (GameController.Instance.getCurrentWavelengthAsString())
        {
            case "RED":
                MakeVisibleCollidable(keys[0]);
                ClearVisibleCollidable(keys[1]);
                ClearVisibleCollidable(keys[2]);
                break;
            case "GREEN":
                ClearVisibleCollidable(keys[0]);
                MakeVisibleCollidable(keys[1]);
                ClearVisibleCollidable(keys[2]);
                break;
            case "BLUE":
                ClearVisibleCollidable(keys[0]);
                ClearVisibleCollidable(keys[1]);
                MakeVisibleCollidable(keys[2]);
                break;
        }
    }

    private void MakeVisibleCollidable(GameObject keyObject)
    {
        if (keyObject != null)
        {
            keyObject.GetComponent<Renderer>().enabled = true;
            keyObject.GetComponent<Collider>().enabled = true;
        }
    }


    private void ClearVisibleCollidable(GameObject keyObject)
    {
        if (keyObject != null)
        {
            keyObject.GetComponent<Renderer>().enabled = false;
            keyObject.GetComponent<Collider>().enabled = false;
        }
    }


}
