using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/TargetNotVisiable")]
public class VisiableTargetDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return TargetNotVisiable(controller);
    }

    private bool TargetNotVisiable(StateController controller)
    {
        controller.transform.Rotate(0, controller.EnemyStats.SearchingTurnSpeed * Time.deltaTime, 0);
        return controller.HasTimeElapsed(controller.EnemyStats.SearchDuration);
    }
}
