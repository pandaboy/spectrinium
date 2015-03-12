using UnityEngine;
using System.Collections;

public class GoToPlayScene : MonoBehaviour {

    void OnClick()
    {
        Application.LoadLevel("sceneTestByWalt");
        Time.timeScale = 1.0f;
    }
}
