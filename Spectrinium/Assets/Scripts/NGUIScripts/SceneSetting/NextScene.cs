using UnityEngine;
using System.Collections;

public class NextScene : MonoBehaviour {

	void Update () {
	
	}

    void OnClick()
    {
        Application.LoadLevel("SettingScene");
		Cursor.visible = true;
    }
    void Start()
    {
        StartCoroutine(WaitAndPrint(4.0F));
       
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel("SettingScene");
    }
}
