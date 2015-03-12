using UnityEngine;
using System.Collections;

public class Recoil : MonoBehaviour
{
    public GameObject weapon;
    public Transform recoilMod;

    public float maxRecoil_z = -10.0f;
    public float recoilSpeed = 5.0f;
    public float recoil = 0.0f;
	
	void Update ()
    {
		if(Input.GetButtonDown("Fire1")) {
            recoil += 0.1f;
        }

        Recoiling();
	}

    void Recoiling()
    {
        if (recoil > 0.0f)
        {
            var maxRecoil = Quaternion.Euler(0, 0, maxRecoil_z);

            recoilMod.rotation = Quaternion.Slerp(recoilMod.rotation, maxRecoil, Time.deltaTime * recoilSpeed);
            weapon.transform.localEulerAngles = new Vector3(
                weapon.transform.localEulerAngles.x,
                weapon.transform.localEulerAngles.y,
                recoilMod.localEulerAngles.z
            );
            recoil -= Time.deltaTime;
        }
        else
        {
            recoil = 0.0f;
            var minRecoil = Quaternion.Euler(0, 0, 0);

            recoilMod.rotation = Quaternion.Slerp(recoilMod.rotation, minRecoil, Time.deltaTime * recoilSpeed / 2);
            weapon.transform.localEulerAngles = new Vector3(
                weapon.transform.localEulerAngles.x,
                weapon.transform.localEulerAngles.y,
                recoilMod.localEulerAngles.z
            );
        }
    }
}
