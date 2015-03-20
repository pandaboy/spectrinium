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
        if (Input.GetKeyDown("p"))
        {

            Time.timeScale = 0.7f;
            PauseMenu.SetActive(true);
            FPSController.SetActive(false);
            Cursor.visible = true;
            // Cursor.visible = true;

        }
        else
        {
            Time.timeScale = 1;
        }
        if (Input.GetKey("o"))
        {
            PauseMenu.SetActive(false);
            FPSController.SetActive(true);
            Cursor.visible = false;
            // Cursor.visible = true;
            Time.timeScale = 1;
        }
	}
}
