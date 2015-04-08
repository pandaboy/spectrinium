using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementWindow : MonoBehaviour {
	public AchievementNotification achievementNotification;
	public UILabel unlockProgressLabel;
	public List <string> achievements;
	public List <UISprite> achievementUIs = new List<UISprite> ();

	// Use this for initialization
	void Start () {
		achievements = new List<string> (new string[] {
			"Application Launched", //#0 open a menu
			"No Sound in Space", 	//#1 music or sound more than 0.8
			"Titanium Alloy", 		//#2 switch theme
			"Perfectionist",		//#3 press replay button
			"Warp Drive Activated",	//#4 skip mission
			"Bellicist",			//#5 switch to weapon panel
			"Keep It Tidy",			//#6 press reset button
			"Mechanic",				//#7 press any button on upgrade window
			"Deep Reader",			//#8 scroll the label of description
			"Achiever",				//#9 unlock all the achievements
		});
	}

	public void UpdateAchievementWindow () {
		for (int i = 0; i < achievements.Count; i++) {
			achievementUIs [i].GetComponentInChildren <UILabel> ().text = achievements [i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent <NGUIExample> ().descriptionPanel.gameObject.activeSelf) {
			foreach (int i in achievementNotification.informedAchievements) {
				if (achievementUIs [i].transform.FindChild ("Checkmark").GetComponentInChildren <UISprite> ().spriteName != "Check_success") {
					achievementUIs [i].transform.FindChild ("Checkmark").GetComponentInChildren <UISprite> ().spriteName = "Check_success";
				}
			}
			unlockProgressLabel.text = "You have unlocked " + achievementNotification.informedAchievements.Count + "/10";
		}

	}
}
