using UnityEngine;
using System.Collections;

public class pauseBarController : MonoBehaviour {
    public GameObject PauseMenu;
    public GameObject FPSController;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey("p"))
        {
            PauseMenu.SetActive(true);
            FPSController.SetActive(false);
  //          Cursor.visible = true;
            Screen.showCursor = true;
            Time.timeScale = 0;
        }
	}
}
