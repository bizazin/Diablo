using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/PatrolAction")]
public class PatrolAction : StateAction
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        Enemy enemy = controller.GetComponent<Enemy>();
        enemy.currentState = CurrentState.Patrol;
        controller.Agent.speed = controller.EnemyStats.WalkSpeed;
        controller.Agent.destination = controller.Waypoints[controller.NextWaypoint].position;
        controller.Agent.isStopped = false;
        if(controller.Agent.remainingDistance <= controller.Agent.stoppingDistance && !controller.Agent.pathPending)
        {
            controller.NextWaypoint = (controller.NextWaypoint + 1) % controller.Waypoints.Count;
        }
    }
}
