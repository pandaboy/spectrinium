using UnityEngine;
using System.Collections;

public class FrozenUI_ProgressBar_Ending : MonoBehaviour {

	public UIWidget target;
	public UIProgressBar bar;
	public int offset = 0;

	public bool fading = true;
	public float fadeDuration = 0.5f;

	private int defaultWidth = 0;
	private float defaultAlpha = 1f;

	void Start ()
	{
		if (this.target == null || this.bar == null)
		{
			Debug.LogWarning(this.GetType() + " requires target UIWidget and UIProgressBar in order to work.", this);
			this.enabled = false;
			return;
		}

		// Get the default with of the target widget
		this.defaultWidth = this.target.width;

		// Get the default alpha of the target widget
		this.defaultAlpha = this.target.alpha;

		// Hook on change event
		this.bar.onChange.Add(new EventDelegate(OnBarChange));
	}

	void OnBarChange()
	{
		// Calculate the bar fill based on it's width and value
		int fillWidth = Mathf.CeilToInt((float)this.bar.foregroundWidget.width * this.bar.value);

		// Check if the fill width is too small to bother with the ending
		if (fillWidth <= (1 + this.offset))
		{
			this.target.enabled = false;
			return;
		}
		else if (!this.target.enabled)
		{
			// Re-enable
			this.target.enabled = true;
		}

		// Position the ending at the end of the fill
		this.target.transform.localPosition = new Vector3(
			((this.bar.foregroundWidget.cachedTransform.localPosition.x + (float)fillWidth) - this.offset), 
			this.target.cachedTransform.localPosition.y, 
			this.target.cachedTransform.localPosition.z
		);

		// Check if the fill width is too great to handle the ending width
		if (fillWidth < this.defaultWidth)
		{
			// Change the width to the fill width
			this.target.width = fillWidth;
		}
		else if (this.target.width != this.defaultWidth)
		{
			// Restore default width
			this.target.width = this.defaultWidth;
		}

		// Fading
		if (this.fading)
		{
			if (this.bar.value == 1f)
			{
				// Fade out at 100%
				TweenAlpha.Begin(this.target.gameObject, this.fadeDuration, 0f).method = UITweener.Method.EaseOut;
			}
			else if (this.target.alpha == 0f)
			{
				// Fade in if not 100%
				TweenAlpha.Begin(this.target.gameObject, this.fadeDuration, this.defaultAlpha).method = UITweener.Method.EaseIn;
			}
		}
	}
}
