using UnityEngine;
using System.Collections;

public class FrozenUI_PaginationGlow : MonoBehaviour {

	public UISprite glowSprite;
	public float duration = 1f;

	void Start()
	{
		if (this.glowSprite == null)
		{
			this.enabled = false;
			return;
		}

		// Start fading in
		this.FadeOutComplete();
	}

	void OnClick()
	{
		this.Disable();
	}

	private void FadeInComplete()
	{
		TweenAlpha tween = TweenAlpha.Begin(this.glowSprite.gameObject, this.duration, 0f);
		tween.method = UITweener.Method.EaseInOut;
		tween.onFinished.Add(new EventDelegate(FadeOutComplete));
	}

	private void FadeOutComplete()
	{
		TweenAlpha tween = TweenAlpha.Begin(this.glowSprite.gameObject, this.duration, 1f);
		tween.method = UITweener.Method.EaseInOut;
		tween.onFinished.Add(new EventDelegate(FadeInComplete));
	}

	private void Disable()
	{
		TweenAlpha tween = TweenAlpha.Begin(this.glowSprite.gameObject, this.duration, 0f);
		tween.method = UITweener.Method.EaseInOut;
		tween.onFinished.Clear();
	}
}
