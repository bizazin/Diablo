#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// Draws visual representation of fov
/// </summary>
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.ViewRadius);
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.AttackRadius);

        Vector3 viewAngleA = fov.DirFromAngle(-fov.ViewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.ViewAngle / 2, false);

        Vector3 attackAngleA = fov.DirFromAngle(-fov.AttackAngle / 2, false);
        Vector3 attackAngleB = fov.DirFromAngle(fov.AttackAngle / 2, false);

        Handles.color = Color.white;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.ViewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.ViewRadius);

        Handles.color = Color.red;
        Handles.DrawLine(fov.transform.position, fov.transform.position + attackAngleA * fov.AttackRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + attackAngleB * fov.AttackRadius);

        Handles.color = Color.cyan;

        if (fov.VisibleTarget != null)
        {
            Handles.DrawLine(fov.transform.position, fov.VisibleTarget.position);
        }

        Handles.color = Color.green;
        if(fov.DamageableTarget != null)
        {
            Handles.DrawLine(fov.transform.position, fov.DamageableTarget.position);
        }
    }
}
#endif