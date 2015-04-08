using UnityEngine;
using System.Collections;

public class StoreWindow : MonoBehaviour {
	public UIButton supplyTab;
	public UIButton weaponTab;
	public Color32 tabEnableColor;
	public Color32 tabDisableColor;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable () {
		SwitchToSupply ();
	}

	public void SwitchToWeapon () {
		weaponTab.isEnabled = false;
		supplyTab.isEnabled = true;
		weaponTab.GetComponentInChildren <UILabel> ().color = tabDisableColor;
		supplyTab.GetComponentInChildren <UILabel> ().color = tabEnableColor;
	}

	public void SwitchToSupply () {
		supplyTab.isEnabled = false;
		weaponTab.isEnabled = true;
		weaponTab.GetComponentInChildren <UILabel> ().color = tabEnableColor;
		supplyTab.GetComponentInChildren <UILabel> ().color = tabDisableColor;
	}
}
