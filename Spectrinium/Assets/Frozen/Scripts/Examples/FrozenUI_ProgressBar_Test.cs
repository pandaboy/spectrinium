using UnityEngine;
using System.Collections;

public class FrozenUI_ProgressBar_Test : MonoBehaviour {

	public UIProgressBar bar;
	public UILabel label;
	public FrozenUI_Easing.EasingType easing = FrozenUI_Easing.EasingType.easeLinear;
	public float duration = 2f;
	public float startDelay = 0f;
	public float holdTime = 1f;

	private bool started = false;
	private bool ascending = false;
	
	void Start()
	{
		if (this.bar == null)
			this.bar = this.GetComponent<UIProgressBar>();
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

			// Invert in case of decending
			if (!this.ascending)
				eased = 1f - eased;

			this.bar.value = eased;

			if (this.label != null)
				this.label.text = (this.bar.value * 100).ToString("0") + "%";
			
			yield return 0;
		}

		// Round up the value
		this.bar.value = (this.ascending) ? 1f : 0f;

		if (this.holdTime > 0f)
			yield return new WaitForSeconds(this.holdTime);
		
		this.ascending = !this.ascending;
		this.StartCoroutine("Progress");
	}
}
