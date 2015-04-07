using UnityEngine;
using System.Collections;

public class PerFrameRaycast : MonoBehaviour {

    private RaycastHit hit;
	
    void Update()
    {
        // hit something
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Physics.Raycast(transform.position, fwd, out hit, 100.0F);
	}

    public RaycastHit GetInfo()
    {
        return hit;
    }
}
