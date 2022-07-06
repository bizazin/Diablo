
using System.Collections.Generic;
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
    [Range(0, 360)]
    [SerializeField] private float viewAngle;
    [SerializeField] private float viewRadius;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [SerializeField] private float findTargetsDelay;
    public Transform visibleTarget;

    public float ViewRadius
    {
        get => viewRadius;
    }
    public float ViewAngle
    {
        get => viewAngle;
    }

    private void FixedUpdate()
    {
        FindVisibleTargets();
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void FindVisibleTargets()
    {
        visibleTarget = null;
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
                {
                    visibleTarget = target;
                }
            }
        }
    }
}
