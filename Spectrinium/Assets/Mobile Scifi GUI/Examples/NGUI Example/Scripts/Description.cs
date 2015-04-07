using UnityEngine;
using System.Collections;

public class Description : MonoBehaviour {
	public AchievementNotification AN;
	public UIPanel scrollView;
	private Vector2 initialClippingOffset;

	public void MailToGameCube () {
		Application.OpenURL ("");
	}
	
	public void GoToTwitter () {
		Application.OpenURL ("");
	}
	
	public void GoToFaceBook () {
        Application.OpenURL("https://www.facebook.com/groups/1393186187649461/?fref=nf");
	}
	
	public void LoadDFGUIExample () {
		Application.LoadLevel ("DFGUI Example");
	}

    public void StartGame()
    {
        Application.LoadLevel("sceneTestByWalt");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void VolumnControl(float volumnControl)
    {
        GetComponent<AudioSource>().volume = 0.1f;
    }

	void Start () {
		initialClippingOffset = scrollView.clipOffset;
	}


	void Update () {
		//if (scrollView.clipOffset != initialClippingOffset) {
		//	AN.AddAchievementReader ();
		//}
	}
}
