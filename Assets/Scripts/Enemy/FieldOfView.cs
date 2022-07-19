using UnityEngine;

/// <summary>
/// Fow calculations
/// </summary>

#if UNITY_EDITOR
[ExecuteInEditMode]
[UnityEditor.CanEditMultipleObjects]
#endif
public class FieldOfView : MonoBehaviour
{
    [Header("View Settings")]
    [Range(0, 360)][SerializeField] private float viewAngle;
    [SerializeField] private float viewRadius;

    [Header("Attack Settings")]
    [Range(0, 360)][SerializeField] private float attackAngle;
    [SerializeField] private float attackRadius;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    public Transform DamageableTarget;
    public Transform VisibleTarget;

    public float ViewRadius
    {
        get => viewRadius;
    }
    public float ViewAngle
    {
        get => viewAngle;
    }

    public float AttackRadius
    {
        get => attackRadius;
    }

    public float AttackAngle
    {
        get => attackAngle;
    }

    private void FixedUpdate()
    {
        FindVisibleTargets();
        FindDamageableTargets();
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void FindVisibleTargets()
    {
        VisibleTarget = null;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                // If there are no obstacles on the way to target
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                    VisibleTarget = target;
            }
        }
    }

    private void FindDamageableTargets()
    {
        DamageableTarget = null;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, attackRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                // If there are no obstacles on the way to target
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    DamageableTarget = target;
                }
            }
        }
    }
}
