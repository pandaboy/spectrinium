// credit: wiki.unity3d.com/index.php/Headbobber
using UnityEngine;
using System.Collections;

public class HeadBobber : MonoBehaviour {

	private float timer = 0.0f;
	public float bobbing_speed = 0.18f;
	public float bobbing_amount = 0.2f;
	public float mid_point = 1.0f;
	
	// Update is called once per frame
	void Update () {
		float wave_slice = 0.0f;

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if ( Mathf.Abs (horizontal) == 0 && Mathf.Abs(vertical) == 0 ) {
			timer = 0.0f;
		}
		else {
			wave_slice = Mathf.Sin (timer);
			timer += bobbing_speed;
			if(timer > Mathf.PI * 2) {
				timer -= (Mathf.PI * 2);
			}
		}

		if ( wave_slice != 0 ){
			float translate_change = wave_slice * bobbing_amount;
			float total_axes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
			total_axes = Mathf.Clamp (total_axes, 0.0f, 1.0f);
			translate_change = total_axes * translate_change;
			transform.localPosition = new Vector3(transform.localPosition.x, mid_point + translate_change, transform.localPosition.z);
			//this.transform.localPosition.y = mid_point + translate_change;
		} else {
			//transform.localPosition.y = mid_point;
			transform.localPosition = new Vector3(transform.localPosition.x, mid_point, transform.localPosition.z);
		}
	}
}
