using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementNotification : MonoBehaviour {
	public AchievementWindow achievementWindow;
	public List <int> uninformedAchievements = new List<int> ();
	public List <int> informedAchievements = new List<int> ();
	public UILabel achievementLabel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (uninformedAchievements.Count != 0 && !gameObject.GetComponent<Animation>().isPlaying) {
			InformAchievement (uninformedAchievements[0]);
			informedAchievements.Add (uninformedAchievements [0]);
			uninformedAchievements.RemoveAt (0);
		}

		if (informedAchievements.Count == 9) {
			AddAchievementUnlockAll ();
		}
	}

	// Call this to add an achievement
	public void AddAchievement (int achievementID) {
		if (!informedAchievements.Contains (achievementID) && !uninformedAchievements.Contains (achievementID)) {
			uninformedAchievements.Add (achievementID);
		}
	}

	void InformAchievement (int achievementID) {
		achievementLabel.gameObject.GetComponent <NotificationTextFX> ().enabled = false;
		achievementLabel.text = achievementWindow.achievements [achievementID];
		gameObject.GetComponent<Animation>().Play ();
		achievementLabel.gameObject.GetComponent <NotificationTextFX> ().enabled = true;
	}

	public void AddAchievementReplay () {
		AddAchievement (3);
	}

	public void AddAchievementSkipMission () {
		AddAchievement (4);
	}

	public void AddAchievementBuyWeapon () {
		AddAchievement (5);
	}

	public void AddAchievementResetWindow () {
		AddAchievement (6);
	}

	public void AddAchievementUpgrade () {
		AddAchievement (7);
	}

	public void AddAchievementReader () {
		AddAchievement (8);
	}

	public void AddAchievementUnlockAll () {
		AddAchievement (9);
	}
}
