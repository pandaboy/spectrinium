using UnityEngine;
using System.Collections;

public class pauseBarController : MonoBehaviour {
    public GameObject PauseMenu;
    public GameObject music;
    public GameObject FPSController;

    private bool showPause = false;
    CursorLockMode wantedMode;

	// Use this for initialization
	void Start () {
         
	}
	
	// Update is called once per frame
	void Update () {

        Cursor.lockState = wantedMode;
        if (Input.GetKeyDown("p"))
        {
          //  Time.timeScale = 1.0f;
          //  Application.LoadLevel("PauseSettingScene");
            if (!showPause)
            {
                Time.timeScale = 0.0f;
                PauseMenu.SetActive(true);
                music.SetActive(true);
                FPSController.SetActive(false);
                Cursor.visible = true;
                // Cursor.visible = true;

                showPause = true;
            }
            else
            {
                PauseMenu.SetActive(false);
                FPSController.SetActive(true);
                Cursor.visible = false;
                // Cursor.visible = true;
                music.SetActive(false);
                Time.timeScale = 1.0f;

                showPause = false;
            }
            
        }
        if (Input.GetKeyDown("m"))
        {
            Application.LoadLevel("SettingScene");
        }

	}
}
