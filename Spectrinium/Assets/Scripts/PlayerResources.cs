using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour
{
    public float health;
    public float spectrinium;

    public int startingHealth;
    public int startingSpectrinium;

    private int maxHealth;
    private int maxSpectrinium;

    public int fireSpecCost;
    public int switchSpecCost;

    public bool hasRedKey = false;
    public bool hasGreenKey = false;
    public bool hasBlueKey = false;

    static float healValues;
	static float specValues;

	public GameObject KeyRed;
	public GameObject KeyGreen;
	public GameObject KeyBlue;

	public GameObject Door;

    void Start()
    {
        health = maxHealth = startingHealth;
		healValues = health;
        spectrinium = maxSpectrinium = startingSpectrinium;
		specValues = spectrinium;

    }

    public void CollectSpectrinium(int spec)
    {
        spectrinium += spec;

        if (spectrinium > maxSpectrinium)
            spectrinium = maxSpectrinium;

		specValues = spectrinium;
    }

    public bool SpendSpectrinium(int cost)
    {
        if (cost <= spectrinium)
        {
            spectrinium -= cost;
			specValues = spectrinium;
            return true;
        }

        return false;
    }

    public bool FireSpectrinium()
    {
        return SpendSpectrinium(fireSpecCost);
    }

    public bool SwitchSpectrinium()
    {
        return SpendSpectrinium(switchSpecCost);
    }

    public void CollectKey(string wavelength)
    {
        if (wavelength == "Red") {
			hasRedKey = true;
			KeyRed.SetActive (true);
		}
        if (wavelength == "Green") {
			hasGreenKey = true;
			KeyGreen.SetActive(true);
		}
        if (wavelength == "Blue") {
			hasBlueKey = true;
			KeyBlue.SetActive(true);
		}

        if (CheckHasKeys ())
			DoorAppear ();
            //GameController.Instance.UnlockExit();
    }

    private bool CheckHasKeys()
    {
        if ((hasRedKey) && (hasGreenKey) && (hasBlueKey))
            return true;

        return false;
    }

    public void Shot(float damage)
    {
        health -= damage;
		healValues = health;
        if (health <= 0)
            GameController.Instance.PlayerDead();
    }

	public static float GetPlayerHealth()
	{
		return healValues;
	}
	public static float GetPlayerSpec()
	{
		return specValues;
	}

	public void DoorAppear()
	{
		Door.SetActive (true);
	}

}
