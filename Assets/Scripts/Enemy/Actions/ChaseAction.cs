using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/ChaseAction")]
public class ChaseAction : StateAction
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        Enemy enemy = controller.GetComponent<Enemy>();
        enemy.CurState = CurrentState.Attack;
        controller.Agent.speed = controller.EnemyStats.RunSpeed;
        FieldOfView fov = controller.GetComponent<FieldOfView>();
        if (fov == null) return;
        if (fov.VisibleTarget != null)
        {
            controller.Agent.destination = controller.Target.position;
            controller.LastKnowTargetPostition = controller.Target.position;
        }
        else
            controller.Agent.destination = controller.LastKnowTargetPostition;

        controller.Agent.isStopped = false;
    }
}
