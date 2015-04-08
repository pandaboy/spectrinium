using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour
{
    public float deadZone = 5.0f;

    private NavMeshAgent nav;
    private Animator anim;
    private EnemyAnimatorSetup animSetup;
    private EnemyAI enemyAI;

    private bool navReady = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();

        animSetup = new EnemyAnimatorSetup(anim);

       

        anim.SetLayerWeight(1, 1.0f);
        anim.SetLayerWeight(2, 1.0f);

        deadZone *= Mathf.Deg2Rad;
    }

    void Update()
    {
        if(navReady)
            NavAnimSetup();
    }

    void OnAnimatorMove()
    {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }
    

    private void NavAnimSetup()
    {
        float speed, angle;
        speed = angle = 0.0f;

        if (enemyAI.playerInRange)
            ShootAnim(ref speed, ref angle);
        else
            MoveAnim(ref speed, ref angle);

        animSetup.Setup(speed, angle);
    }

    public void ShootAnim(ref float speed, ref float angle)
    {
        speed = 0.0f;
        angle = FindAngle(transform.forward, enemyAI.lastSeen - transform.position, transform.up);
    }

    public void MoveAnim(ref float speed, ref float angle)
    {
        speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;

        angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

        if (angle < deadZone)
        {
            transform.LookAt(transform.position + nav.desiredVelocity);
            angle = 0.0f;
        }
    }

    private float FindAngle(Vector3 from, Vector3 to, Vector3 up)
    {
        if (to == Vector3.zero)
            return 0.0f;

        float angle = Vector3.Angle(from, to);

        Vector3 normal = Vector3.Cross(from, to);

        angle *= Mathf.Sign(Vector3.Dot(normal, up));
        angle *= Mathf.Deg2Rad;

        return angle;
    }


    public void SetupNavMeshAgent()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.updateRotation = false;

        navReady = true;
    }
}
