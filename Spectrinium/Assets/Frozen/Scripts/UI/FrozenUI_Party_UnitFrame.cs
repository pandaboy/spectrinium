using UnityEngine;
using System.Collections;

public class FrozenUI_Party_UnitFrame : MonoBehaviour {

	public enum State
	{
		None,
		Resting,
		InCombat,
		Offline,
	}

	private const string restingOverlay = "PartyUnitFrame_RestingOverlay";
	private const string combatOverlay = "PartyUnitFrame_CombatOverlay";
	private const string normalLevel = "PartyUnitFrame_Level_Background";
	private const string disconnectedLevel = "PartyUnitFrame_Level_Disconnected";

	public State startingState;
	public UIWidget container;
	public UISprite offlineOverlay;
	public UISprite stateOverlay;
	public UISprite levelContainer;
	public UILabel levelLabel;

	private State currentState = State.None;

	void Start()
	{
		if (this.container == null)
			this.container = this.GetComponent<UIWidget>();

		// Set the starting state
		this.SetState(this.startingState);
	}

	public void SetState(State state)
	{
		if (this.currentState == state)
			return;

		if (state == State.Resting || state == State.InCombat)
		{
			// Set the sprite
			this.stateOverlay.spriteName = (state == State.Resting) ? restingOverlay : combatOverlay;

			// Enable the state overlay
			this.stateOverlay.enabled = true;
		}
		else
		{
			// Disable the state overlay
			this.stateOverlay.enabled = false;
		}

		// Set the offline state
		this.SetOffline(state == State.Offline);

		// Save the state as current
		this.currentState = state;
	}
	
	private void SetOffline(bool state)
	{
		if (state)
		{
			// Enable the offline overlay
			if (this.offlineOverlay != null)
				this.offlineOverlay.enabled = true;

			// Change the level
			if (this.levelLabel != null)
				this.levelLabel.enabled = false;

			if (this.levelContainer != null)
				this.levelContainer.spriteName = disconnectedLevel;
		}
		else
		{
			// Disable the offline overlay
			if (this.offlineOverlay != null)
				this.offlineOverlay.enabled = false;

			// Change the level
			if (this.levelLabel != null)
				this.levelLabel.enabled = true;
			
			if (this.levelContainer != null)
				this.levelContainer.spriteName = normalLevel;
		}
	}
}
