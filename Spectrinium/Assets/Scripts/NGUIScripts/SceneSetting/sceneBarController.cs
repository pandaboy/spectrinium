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
		float playerHealth = PlayerResources.GetPlayerHealth ();
        float UpdateHealth_f = Health_f * 100;
		float divideValue = playerHealth / 100;
		if (playerHealth >= 0f && (playerHealth <= 100f))
        {
			GameObject.Find("HealthBar").GetComponent<UIProgressBar>().value = divideValue;
			Health_Lable.text = (playerHealth.ToString() + ("% Health"));
        }
        
        else
        {
        }
	}

}
