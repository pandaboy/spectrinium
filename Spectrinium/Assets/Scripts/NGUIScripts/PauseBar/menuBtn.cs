using UnityEngine;
using System.Collections;

public class menuBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnClick()
    {
        Time.timeScale = 0;
        Application.LoadLevel("SettingScene");
    }
}
