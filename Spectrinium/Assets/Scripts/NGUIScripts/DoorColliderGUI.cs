using UnityEngine;
using System.Collections;

public class DoorColliderGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		GameObject otherObject = other.gameObject;
		
		if (otherObject.tag == "Player")
		{
			GameController.Instance.UnlockExit();
		}
	}
}
