using UnityEngine;
using System.Collections;
using UnityEditor;

public class Wall : MonoBehaviour {
    /*
    public static int insideRed = 0;
    public static int insideGreen = 0;
    public static int insideBlue = 0;
    */
    private static bool insideRed = false;
    private static bool insideGreen = false;
    private static bool insideBlue = false;

    void Start()
    {
        //make walls navigation static for navmesh - SARAH
        GameObjectUtility.SetStaticEditorFlags(gameObject, StaticEditorFlags.NavigationStatic);
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.transform.parent == Map.red_group.transform)
                insideRed++;
            else if (this.transform.parent == Map.green_group.transform)
                insideGreen++;
            else if (this.transform.parent == Map.blue_group.transform)
                insideBlue++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.transform.parent == Map.red_group.transform)
                insideRed--;
            else if (this.transform.parent == Map.green_group.transform)
                insideGreen--;
            else if (this.transform.parent == Map.blue_group.transform)
                insideBlue--;
        }
    }

    public static bool CanSwitch(string wavelength)
    { 
        if (wavelength == "RED")
            return (insideRed == 0);
        else if (wavelength == "GREEN")
            return (insideGreen == 0);
        else //"BLUE"
            return (insideBlue == 0);
    }
	*/

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.transform.parent == Map.red_group.transform)
                insideRed = true;

            if (this.transform.parent == Map.green_group.transform)
                insideGreen = true;

            if (this.transform.parent == Map.blue_group.transform)
                insideBlue = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.transform.parent == Map.red_group.transform)
                insideRed = false;

            if (this.transform.parent == Map.green_group.transform)
                insideGreen = false;

            if (this.transform.parent == Map.blue_group.transform)
                insideBlue = false;
        }
    }

    public static bool CanSwitch(string wavelength)
    {
        if (wavelength == "RED")
            return !insideRed;
        else if (wavelength == "GREEN")
            return !insideGreen;
        else //"BLUE"
            return !insideBlue;
    }
}
