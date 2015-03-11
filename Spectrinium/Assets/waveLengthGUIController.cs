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
		float UpdateWaveLength_f = waveLength_f * 100;
		if (UpdateWaveLength_f >= 0f && (UpdateWaveLength_f <= 100f))
		{
			GameObject.Find("waveLengthBar").GetComponent<UIProgressBar>().value = waveLength_f;
			waveLength_Lable.text = (UpdateWaveLength_f.ToString() + ("% wavelength"));
		}
		
		else
		{
		}
	}
}
