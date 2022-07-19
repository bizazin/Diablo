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

        if (!controller.StateBool)
        {
            controller.stateTimeElapsed = controller.EnemyStats.AttackRate;
            controller.StateBool = true;
        }

        if (fov.DamageableTarget != null)
        {
            if (controller.Agent.remainingDistance <= controller.Agent.stoppingDistance)
            {
                controller.Agent.speed = 0f;
                controller.Agent.isStopped = true;

                if (fov.DamageableTarget != null && controller.HasTimeElapsed(controller.EnemyStats.AttackRate))
                    controller.GetComponent<EnemyAnimationController>().AnimateAttack(true);
            }
        }
        controller.Agent.speed = controller.EnemyStats.RunSpeed;
    }
}
