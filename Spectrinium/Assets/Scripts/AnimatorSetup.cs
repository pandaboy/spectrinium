using UnityEngine;
using System.Collections;

public class AnimatorSetup
{
    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleResponseTime = 0.6f;

    private Animator anim;

    public AnimatorSetup(Animator animator)
    {
        anim = animator;
    }

    public void Setup(float speed, float angle)
    {
        float angularSpeed = angle / angleResponseTime;

        anim.SetFloat(0, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(0, angularSpeed, angularSpeedDampTime, Time.deltaTime);
    }
}
