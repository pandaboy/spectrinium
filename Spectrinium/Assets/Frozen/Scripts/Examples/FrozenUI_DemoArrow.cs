using UnityEngine;
using System.Collections;

public class FrozenUI_DemoArrow : MonoBehaviour {

	public UIScrollView scrollView;
	public float disableOnOffsetY = 0f;
	public bool disableOnHide = true;
	private UISprite sprite;

	private bool isShown = false;

	void Start ()
	{
		this.sprite = this.GetComponent<UISprite>();

		if (this.scrollView == null || this.sprite == null)
		{
			this.enabled = false;
			return;
		}

		if (!this.isShown)
			this.sprite.alpha = 0f;
	}

	void Update()
	{
		if (this.scrollView == null)
			return;

		if (this.isShown && this.scrollView.transform.localPosition.y >= this.disableOnOffsetY)
		{
			TweenAlpha.Begin(this.sprite.gameObject, 1f, 0f).onFinished.Add(new EventDelegate(OnHide));
			this.isShown = false;
		}
		else if (!this.isShown && this.scrollView.transform.localPosition.y < this.disableOnOffsetY)
		{
			TweenAlpha.Begin(this.sprite.gameObject, 1f, 1f);
			this.isShown = true;
		}
	}

	void OnHide()
	{
		if (this.disableOnHide)
			this.gameObject.SetActive(false);
	}
}
