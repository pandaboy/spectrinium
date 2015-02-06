using UnityEngine;
using System.Collections;

public class Crosshair_Script : MonoBehaviour {

    public float MoveAmount = 1;
     public float MoveSpeed = 2;

    public GameObject GUN;
     public float MoveOnX ;
     public float MoveOnY ;
    public Vector3 DefaultPos;
    public Vector3 NewGunPos;


	// Use this for initialization
	void Start () {
        DefaultPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        MoveOnX = Input.GetAxis("Mouse X") * Time.deltaTime * MoveAmount;

        MoveOnY = Input.GetAxis("Mouse Y") * Time.deltaTime * MoveAmount;

        NewGunPos = new Vector3(DefaultPos.x + MoveOnX, DefaultPos.y + MoveOnY, DefaultPos.z);

        GUN.transform.localPosition = Vector3.Lerp(GUN.transform.localPosition, NewGunPos, MoveSpeed * Time.deltaTime);
	}
}
