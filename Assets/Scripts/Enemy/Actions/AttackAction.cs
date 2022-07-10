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
        if(fov.damageableTarget != null)
        {
            if (controller.Agent.remainingDistance <= controller.Agent.stoppingDistance) 
            {
                controller.Agent.speed = 0f;
                controller.Agent.isStopped = true;
                if (fov.damageableTarget != null && fov.damageableTarget.TryGetComponent(out IDamageable damageable) && controller.HasTimeElapsed(controller.EnemyStats.AttackRate))
                {
                    controller.GetComponent<EnemyAnimationController>().AnimateAttack(true);
                    //damageable.ApplyDamage(controller.EnemyStats.Damage);
                }
            }
        }
        controller.Agent.speed = controller.EnemyStats.RunSpeed;
    }
}
