using UnityEngine;
using System.Collections;

public class NGUIThemeSwitch : MonoBehaviour {
	public string currentTheme = "Theme01";
	public UIAtlas Atlas01;
	public UIAtlas Atlas02;

	public Texture2D BGDay;
	public Texture2D BGNight;

	public Color32 fontColor01;
	public Color32 fontColor02;
	public Color32 fontEffectColor01;
	public Color32 fontEffectColor02;

	public Color32 buttonFontColor01;
	public Color32 buttonFontColor02;
	public Color32 buttonFontEffectColor01;
	public Color32 buttonFontEffectColor02;

	public Color32 textFontColor01;
	public Color32 textFontColor02;
	public Color32 textFontEffectColor01;
	public Color32 textFontEffectColor02;

	public Color32 fieldFontColor01;
	public Color32 fieldFontColor02;
	public Color32 fieldFontEffectColor01;
	public Color32 fieldFontEffectColor02;

	public Color32 achievementFontColor01;
	public Color32 achievementFontColor02;

	public Color32 levelFontColor01;
	public Color32 levelFontColor02;

	public Color32 titleFontColor01;
	public Color32 titleFontColor02;
	

	public void SwitchTheme02 () {
		gameObject.GetComponent <AchievementWindow> ().achievementNotification.AddAchievement (2);
		GameObject[] UISprites;
		UISprites = GameObject.FindGameObjectsWithTag ("NGUI");
		foreach (GameObject go in UISprites) {
			if (go.GetComponent <UISprite> () != null) {
				go.GetComponent <UISprite> ().atlas = Atlas02;
			}
			if (go.GetComponent <UILabel> () != null) {
				if (go.GetComponent <UILabel> ().name == "Label") {
					go.GetComponent <UILabel> ().color = fontColor02;
					go.GetComponent <UILabel> ().effectColor = fontEffectColor02;
				}

				if (go.GetComponent <UILabel> ().name == "ButtonLabel" ||go.GetComponent <UILabel> ().name == "Price") {
					go.GetComponent <UILabel> ().color = buttonFontColor02;
					go.GetComponent <UILabel> ().effectColor = buttonFontEffectColor02;
				}

				if (go.GetComponent <UILabel> ().name == "TextLabel") {
					go.GetComponent <UILabel> ().color = fieldFontColor02;
					go.GetComponent <UILabel> ().effectColor = textFontEffectColor02;
				}

				if (go.GetComponent <UILabel> ().name == "FieldLabel") {
					go.GetComponent <UILabel> ().color = textFontColor02;
					go.GetComponent <UILabel> ().effectColor = fieldFontEffectColor02;
				}

				if (go.GetComponent <UILabel> ().name == "AchievementLabel") {
					go.GetComponent <UILabel> ().color = achievementFontColor02;
				}

				if (go.GetComponent <UILabel> ().name == "LevelLabel") {
					go.GetComponent <UILabel> ().color = levelFontColor02;
				}

				if (go.GetComponent <UILabel> ().name == "TitleLabel") {
					go.GetComponent <UILabel> ().color = titleFontColor02;
				}
			}
			if (go.GetComponent <UITexture> () != null) {
				go.GetComponent <UITexture> ().mainTexture = BGNight;
			}
		}
		currentTheme = "Theme02";
	}

	public void SwitchTheme01 () {
		GameObject[] UISprites;
		UISprites = GameObject.FindGameObjectsWithTag ("NGUI");
		foreach (GameObject go in UISprites) {
			if (go.GetComponent <UISprite> () != null) {
				go.GetComponent <UISprite> ().atlas = Atlas01;
			}
			if (go.GetComponent <UILabel> () != null) {
				if (go.GetComponent <UILabel> ().name == "Label") {
					go.GetComponent <UILabel> ().color = fontColor01;
					go.GetComponent <UILabel> ().effectColor = fontEffectColor01;
				}

				if (go.GetComponent <UILabel> ().name == "ButtonLabel") {
					go.GetComponent <UILabel> ().color = buttonFontColor01;
					go.GetComponent <UILabel> ().effectColor = buttonFontEffectColor01;
				}

				if (go.GetComponent <UILabel> ().name == "TextLabel") {
					go.GetComponent <UILabel> ().color = textFontColor01;
					go.GetComponent <UILabel> ().effectColor = textFontEffectColor01;
				}

				if (go.GetComponent <UILabel> ().name == "FieldLabel") {
					go.GetComponent <UILabel> ().color = textFontColor01;
					go.GetComponent <UILabel> ().effectColor = fieldFontEffectColor01;
				}

				if (go.GetComponent <UILabel> ().name == "AchievementLabel") {
					go.GetComponent <UILabel> ().color = achievementFontColor01;
				}

				if (go.GetComponent <UILabel> ().name == "LevelLabel") {
					go.GetComponent <UILabel> ().color = levelFontColor01;
				}

				if (go.GetComponent <UILabel> ().name == "TitleLabel") {
					go.GetComponent <UILabel> ().color = titleFontColor01;
				}
			}
			if (go.GetComponent <UITexture> () != null) {
				go.GetComponent <UITexture> ().mainTexture = BGDay;
			}
		}
		currentTheme = "Theme01";
	}
}
