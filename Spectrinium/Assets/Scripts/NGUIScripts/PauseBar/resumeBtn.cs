using UnityEngine;
using System.Collections;

public class resumeBtn : MonoBehaviour {

    public GameObject PauseMenu;
    public GameObject FPSController;
    private bool showPauseMenu = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnClick()
    {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
        FPSController.SetActive(true);
 //       Cursor.visible = false;
        Cursor.visible = false;
          showPauseMenu = false;
    }
}
