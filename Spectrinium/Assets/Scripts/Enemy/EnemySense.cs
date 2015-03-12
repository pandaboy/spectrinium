using UnityEngine;
using System.Collections;

public class EnemySense : MonoBehaviour
{
    public float hearingRange;
    public float sightRange;
    public float fieldOfView;

    private float prevHearRange;
    private float prevSightRange;
    private float prevFOV;

    private EnemyHear hearing;
    private EnemySight sight;

    void Start()
    {
        hearing = gameObject.GetComponentInChildren<EnemyHear>();
        sight = gameObject.GetComponentInChildren<EnemySight>();
    }

    void FixedUpdate()
    {
        if (hearingRange != prevHearRange)
        {
            hearing.UpdateHearingRange(hearingRange);
            prevHearRange = hearingRange;
        }
        if (sightRange != prevSightRange)
        {
            sight.UpdateSightRange(sightRange);
            prevSightRange = sightRange;
        }

        //assumption - prevents dividing by zero
        if ((fieldOfView != prevFOV)&&(fieldOfView <=179 )&&(fieldOfView >= 1))
        {
            sight.UpdateFieldOfView(fieldOfView);
            prevFOV = fieldOfView;
        }
    }



}
