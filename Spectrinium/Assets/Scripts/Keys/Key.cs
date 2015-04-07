using UnityEngine;
//using UnityEditor;
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

        int d = NavAreas.Walkable;
        int space = NavAreas.Space;
        int R = NavAreas.Red;
        int G = NavAreas.Green;
        int B = NavAreas.Blue;
        int RG = NavAreas.RedGreen;
        int RB = NavAreas.RedBlue;
        int GB = NavAreas.GreenBlue;
        int RGB = NavAreas.RedGreenBlue;

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


            TileArea t = floorObject.GetComponent<TileArea>();
            int tileLayerID = t.navArea;


            int check = thisLayerMask >> tileLayerID;
            if (check % 2 != 0)
            {
                return floorObject.transform.position;
            }
        }

    }

}
