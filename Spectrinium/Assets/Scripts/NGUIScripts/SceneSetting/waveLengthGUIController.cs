using UnityEngine;
using System.Collections;

public class waveLengthGUIController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public UILabel waveLength_Lable;
	public float waveLength_f = 0f;
	
	
	// Update is called once per frame
	void Update () {
		float playerSpec = PlayerResources.GetPlayerSpec ();
		float UpdateWaveLength_f = waveLength_f * 100;
		float divideValue = playerSpec / 100;
		if (playerSpec >= 0f && (playerSpec <= 100f))
		{
			GameObject.Find("waveLengthBar").GetComponent<UIProgressBar>().value = divideValue;
			waveLength_Lable.text = (playerSpec.ToString() + ("% wavelength"));
		}
		
		else
		{
		}
	}
}
