using UnityEngine;
using System.Collections;

public class Spectrinium : MonoBehaviour
{
    public int amountSpec;

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;

        if (otherObject.tag == "Player")
        {
            PlayerResources player = otherObject.GetComponentInParent<PlayerResources>();
            player.CollectSpectrinium(amountSpec);
            Destroy(gameObject);
        }
    }
}
