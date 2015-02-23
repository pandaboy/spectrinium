using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    public static int insideRed = 0;
    public static int insideGreen = 0;
    public static int insideBlue = 0;

    void OnTriggerEnter(Collider other)
    {
        if (this.transform.parent == Map.red_group.transform)
            insideRed++;
        else if (this.transform.parent == Map.green_group.transform)
            insideGreen++;
        else if (this.transform.parent == Map.blue_group.transform)
            insideBlue++;   
    }

    void OnTriggerExit(Collider other)
    {
        if (this.transform.parent == Map.red_group.transform)
            insideRed--;
        else if (this.transform.parent == Map.green_group.transform)
            insideGreen--;
        else if (this.transform.parent == Map.blue_group.transform)
            insideBlue--;
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
	
}
