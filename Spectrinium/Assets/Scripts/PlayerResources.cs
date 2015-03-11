using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour
{
    private int health;
    private int spectrinium;

    public int startingHealth;
    public int startingSpectrinium;

    private int maxHealth;
    private int maxSpectrinium;

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
}
