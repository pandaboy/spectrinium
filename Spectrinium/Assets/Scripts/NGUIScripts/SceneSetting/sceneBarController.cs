using UnityEngine;
using System.Collections;

public class sceneBarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public UILabel Health_Lable;
    public float Health_f = 0f;


	// Update is called once per frame
	void Update () {
        float UpdateHealth_f = Health_f * 100;
        if (UpdateHealth_f >= 0f && (UpdateHealth_f <= 100f))
        {
            GameObject.Find("HealthBar").GetComponent<UIProgressBar>().value = Health_f;
            Health_Lable.text = (UpdateHealth_f.ToString() + ("% Health"));
        }
        
        else
        {
        }
	}
}
