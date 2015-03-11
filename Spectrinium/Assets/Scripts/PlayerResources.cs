using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour
{
    public int health;
    public int spectrinium;

    public int startingHealth;
    public int startingSpectrinium;

    private int maxHealth;
    private int maxSpectrinium;

    public int fireSpecCost;
    public int switchSpecCost;

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
}
