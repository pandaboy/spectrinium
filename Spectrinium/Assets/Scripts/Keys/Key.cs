using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Key : MonoBehaviour
{
    public string wavelength;

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;

        if (otherObject.tag == "Player")
        {
            if(otherObject.layer == LayerMask.NameToLayer(wavelength))
            {
                PlayerResources player = otherObject.GetComponentInParent<PlayerResources>();
                player.CollectKey(wavelength);
                Destroy(gameObject);
            }
        }
    }


    private int GetNavLayer()
    {

        int d = NavMesh.GetAreaFromName("Walkable");
        int space = NavMesh.GetAreaFromName("Space");
        int R = NavMesh.GetAreaFromName("Red");
        int G = NavMesh.GetAreaFromName("Green");
        int B = NavMesh.GetAreaFromName("Blue");
        int RG = NavMesh.GetAreaFromName("RedGreen");
        int RB = NavMesh.GetAreaFromName("RedBlue");
        int GB = NavMesh.GetAreaFromName("GreenBlue");
        int RGB = NavMesh.GetAreaFromName("RedGreenBlue");

        int layerMask = (1 << d) + (1 << space);

        if (wavelength == "Red")
            layerMask += (1 << G) + (1 << B) + (1 << GB);
        else if (wavelength == "Green")
            layerMask += (1 << R) + (1 << B) + (1 << RB);
        else
            layerMask += (1 << R) + (1 << G) + (1 << RG);

        return layerMask;

    }


    public Vector3 FindRandomClearPosition(List<GameObject> floor_objects)
    {
        int num_floorObjects = floor_objects.Count;
        int thisLayerMask = GetNavLayer();

        while(true)
        {
            int randomNum = Random.Range(0, num_floorObjects);

            GameObject floorObject = floor_objects[randomNum];


            int tileLayerID = GameObjectUtility.GetNavMeshArea(floorObject);


            string tileLayerString = GameObjectUtility.GetNavMeshAreaNames()[tileLayerID];


            int check = thisLayerMask >> tileLayerID;
            if (check % 2 != 0)
            {
                return floorObject.transform.position;
            }
        }

    }

}
