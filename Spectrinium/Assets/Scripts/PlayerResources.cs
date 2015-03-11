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

    public bool FireSpectrinium()
    {
        if (fireSpecCost <= spectrinium)
        {
            spectrinium -= fireSpecCost;
            return true;
        }

        return false;
    }
}
