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
        //OnClick();
	}
    void OnClick()
    {
        Time.timeScale = 1.0f;
       
        FPSController.SetActive(true);
        PauseMenu.SetActive(false);
 //       Cursor.visible = false;
        Cursor.visible = false;
          showPauseMenu = false;
    }
}
