#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// Draws visual representation of fov
/// </summary>
[CustomEditor(typeof(PlayerFieldOfView))]
public class FieldOfViewEditorPlayer : Editor
{
    void OnSceneGUI()
    {
        PlayerFieldOfView fov = (PlayerFieldOfView)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.AttackRadius);


        Vector3 attackAngleA = fov.DirFromAngle(-fov.AttackAngle / 2, false);
        Vector3 attackAngleB = fov.DirFromAngle(fov.AttackAngle / 2, false);
        
        Handles.color = Color.red;
        Handles.DrawLine(fov.transform.position, fov.transform.position + attackAngleA * fov.AttackRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + attackAngleB * fov.AttackRadius);

        Handles.color = Color.cyan;
        

        Handles.color = Color.green;
        if(fov.DamageableTargets.Count > 0)
        {
            for (int i = 0; i < fov.DamageableTargets.Count; i++)
            {
                Handles.DrawLine(fov.transform.position, fov.DamageableTargets[i].position);
            }
        }
    }
}
#endif