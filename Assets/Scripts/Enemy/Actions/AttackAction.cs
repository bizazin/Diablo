using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/AttackAction")]
public class AttackAction : StateAction
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        FieldOfView fov = controller.GetComponent<FieldOfView>();
        if (fov == null) return;
        if (!controller.stateBoolVariable)
        {
            controller.stateTimeElapsed = controller.EnemyStats.AttackRate;
            controller.stateBoolVariable = true;
        }
        if(fov.visibleTarget != null)
        {
            if (controller.HasTimeElapsed(controller.EnemyStats.AttackRate))
            {
                //Attack
            }
        }
    }
}
