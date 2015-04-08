using UnityEngine;
using System.Collections;

public class winGameManager : MonoBehaviour {


	void Start ()
    {
        float time = PlayerPrefs.GetFloat("GameTime");
        string temps = string.Format("{0:0.0}", time);
        GameObject.Find("timers").GetComponent<UILabel>().text = "Time: "+temps;

        int kill = PlayerPrefs.GetInt("Killed");
        string temp = kill.ToString();
        GameObject.Find("FieldLabel").GetComponent<UILabel>().text = "Killed: " + temp;
	}
	
	// Update is called once per frame
	void Update () {

	}

}
