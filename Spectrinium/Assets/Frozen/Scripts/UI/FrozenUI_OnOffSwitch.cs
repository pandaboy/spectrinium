using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIToggle))]
[AddComponentMenu("Frozen UI/On Off Switch")]
public class FrozenUI_OnOffSwitch : MonoBehaviour {

	private UIToggle toggle;
	private EventDelegate eventDelegate;

	public UISprite targetSprite;

	public string onSprite;
	public string offSprite;
	public bool makePixelPerfect = true;

	void Start()
	{
		if (this.toggle == null)
			this.toggle = this.GetComponent<UIToggle>();
		
		if (this.toggle != null)
		{
			this.eventDelegate = new EventDelegate(OnChange);
			this.toggle.onChange.Add(new EventDelegate(OnChange));
		}

		if (this.targetSprite == null)
			this.targetSprite = this.GetComponent<UISprite>();

		// Make sure the starting sprite is the correct one
		if (this.targetSprite != null)
			this.onSprite = this.targetSprite.spriteName;

		// Run the onChange to update the toggle initially
		this.OnChange();
	}

	void OnDestroy()
	{
		if (this.toggle != null && this.eventDelegate != null)
			this.toggle.onChange.Remove(this.eventDelegate);
	}

	void OnChange()
	{
		if (!this.enabled || this.targetSprite == null)
			return;

		if (this.toggle.value && !string.IsNullOrEmpty(this.onSprite))
		{
			this.targetSprite.spriteName = this.onSprite;
		}
		else if (!string.IsNullOrEmpty(this.offSprite))
		{
			this.targetSprite.spriteName = this.offSprite;
		}

		if (this.makePixelPerfect)
			this.targetSprite.MakePixelPerfect();
	}
}
