using UnityEngine;
using System.Collections;

public class FrozenUI_Example_Bar : MonoBehaviour {

	public UISprite bar;
	public UILabel label;
	public FrozenUI_Easing.EasingType easing = FrozenUI_Easing.EasingType.easeLinear;
	public float duration = 2f;
	public float startDelay = 0f;
	public float holdTime = 1f;
	public float step = 0f;

	public float[] customSteps;

	private bool started = false;
	private bool ascending = false;

	void Start()
	{
		if (this.bar == null)
			this.bar = this.GetComponent<UISprite>();

		if (this.label == null)
			this.label = this.GetComponentInChildren<UILabel>();
	}

	void OnEnable()
	{
		if (this.bar != null)
			this.StartCoroutine("Progress");
	}

	private IEnumerator Progress()
	{
		if (!this.started && this.startDelay > 0f)
		{
			this.started = true;
			yield return new WaitForSeconds(this.startDelay);
		}

		float startTime = Time.time;

		while (Time.time <= (startTime + this.duration))
		{
			float RemainingTime = ((startTime + this.duration) - Time.time);
			float ElapsedTime = (this.duration - RemainingTime);

			float eased = FrozenUI_Easing.Ease(this.easing, ElapsedTime, 0f, 1f, this.duration);

			if (this.step > 0f)
				eased = (Mathf.Round(eased * (1f / this.step)) / (1f / this.step));

			// Invert in case of decending
			if (!this.ascending)
				eased = 1f - eased;

			// Custom steps
			if (this.customSteps.Length > 0f && eased != 0f && eased != 1f)
			{
				int key = Mathf.RoundToInt(eased * (1f / this.step)) - 1;

				if (key < this.customSteps.Length)
					eased = this.customSteps[key];
			}

			this.bar.fillAmount = eased;

			if (this.label != null)
				this.label.text = (this.bar.fillAmount * 100).ToString("0") + "%";

			yield return 0;
		}

		if (this.holdTime > 0f)
			yield return new WaitForSeconds(this.holdTime);

		this.ascending = !this.ascending;
		this.StartCoroutine("Progress");
	}
}
