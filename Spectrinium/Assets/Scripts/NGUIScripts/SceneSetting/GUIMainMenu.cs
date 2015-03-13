using UnityEngine;
using System.Collections;

public class GUIMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		Cursor.visible = true;
		Application.LoadLevel ("SettingScene");
	}
}
