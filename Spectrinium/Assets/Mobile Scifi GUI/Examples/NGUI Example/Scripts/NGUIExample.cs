using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NGUIExample : MonoBehaviour {
	public List <UISlider> bars = new List<UISlider> ();
	public UIPanel homePanel;
	public UIPanel resultPanel;
	public UIPanel storePanel;
	public UIPanel descriptionPanel;
	public UIPanel startPanel;
	public UIPanel themeSwitchPanel;

	private bool switchPanelDisplay;

	public UIButton homeButton;
	public UIButton storeButton;
	public UIButton resultButton;
	public UIButton descriptionButton;


	void Start () {
		GoToPanel (startPanel);
	}

	// Update is called once per frame
	void Update () {
		foreach (UISlider bar in bars)
			bar.value = Mathf.Sin (Time.time) * 0.5f + 0.5f;
	}

	public void GoToHome () {
		GoToPanel (homePanel);
		UpdateDockButtons (homeButton);
	}

	public void GoToResult () {
		GoToPanel (resultPanel);
		UpdateDockButtons (resultButton);
	}

	public void GoToStore () {
		GoToPanel (storePanel);
		UpdateDockButtons (storeButton);
	}

	public void GoToDescription () {
		GoToPanel (descriptionPanel);
		UpdateDockButtons (descriptionButton);
		gameObject.GetComponent <AchievementWindow> ().UpdateAchievementWindow ();
	}

	void GoToPanel (UIPanel targetPanel) {
		NGUITools.SetActive (startPanel.gameObject, false);
		NGUITools.SetActive (homePanel.gameObject, false);
		NGUITools.SetActive (resultPanel.gameObject, false);
		NGUITools.SetActive (storePanel.gameObject, false);
		NGUITools.SetActive (descriptionPanel.gameObject, false);
		NGUITools.SetActive (targetPanel.gameObject, true);
		if (gameObject.GetComponent <NGUIThemeSwitch> ().currentTheme == "Theme01") {
			gameObject.GetComponent <NGUIThemeSwitch> ().SwitchTheme01 ();
		}
		else {
			gameObject.GetComponent <NGUIThemeSwitch> ().SwitchTheme02 ();
		}

		if (!switchPanelDisplay && targetPanel != startPanel) {
			themeSwitchPanel.enabled = true;
			switchPanelDisplay = true;
		}
	}

	void UpdateDockButtons (UIButton focusButton) {
		homeButton.isEnabled = true;
		storeButton.isEnabled = true;
		resultButton.isEnabled = true;
		descriptionButton.isEnabled = true;
		focusButton.isEnabled = false;
	}
}
