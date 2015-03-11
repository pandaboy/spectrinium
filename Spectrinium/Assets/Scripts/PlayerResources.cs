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

    void Start()
    {
        health = maxHealth = startingHealth;
        spectrinium = maxSpectrinium = startingSpectrinium;
    }

    public void CollectSpectrinium(int spec)
    {
        spectrinium += spec;

        if (spectrinium > maxSpectrinium)
            spectrinium = maxSpectrinium;
    }

    public bool SpendSpectrinium(int cost)
    {
        if (cost <= spectrinium)
        {
            spectrinium -= cost;
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
        if (wavelength == "Red")
            hasRedKey = true;
        if (wavelength == "Green")
            hasGreenKey = true;
        if (wavelength == "Blue")
            hasBlueKey = true;

        if (CheckHasKeys())
            GameController.Instance.UnlockExit();
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

        if (health <= 0)
            GameController.Instance.PlayerDead();
    }
}
