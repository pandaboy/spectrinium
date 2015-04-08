using UnityEngine;
using System.Collections;

public class PerFrameRaycast : MonoBehaviour
{
    private RaycastHit hit;

    private int layerMask = 0;
    private int currentLayerID = -1;


    public void setLayerMask(int layerID)
    {
        currentLayerID = layerID;

        string layerString = LayerMask.LayerToName(layerID);

        int blueLayerID = LayerMask.NameToLayer("Blue");
        int redLayerID = LayerMask.NameToLayer("Red");
        int greenLayerID = LayerMask.NameToLayer("Green");

        int ignoreLayerID = LayerMask.NameToLayer("Ignore Raycast");



        if (layerString == "Red")
            layerMask = (1 << blueLayerID) + (1 << greenLayerID);
        else if (layerString == "Green")
            layerMask = (1 << blueLayerID) + (1 << redLayerID);
        else
            layerMask = (1 << redLayerID) + (1 << greenLayerID);

        layerMask += 1 << ignoreLayerID;

        layerMask = ~layerMask;

    }



    public RaycastHit GetInfo(int layerID)
    {
        if (layerID != currentLayerID)
            setLayerMask(layerID);

        // hit something
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Physics.Raycast(transform.position, fwd, out hit, 100.0f, layerMask);
        return hit;
    }
}
