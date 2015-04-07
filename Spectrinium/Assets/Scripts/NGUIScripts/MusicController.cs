using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    UISlider slider;
    public void VolumnControl()
    {
        GameObject temp = GameObject.Find("Music");
        if (temp != null)
        {
           slider = temp.GetComponent<UISlider>();
        }
        GetComponent<AudioSource>().volume = slider.value;
    }
}
