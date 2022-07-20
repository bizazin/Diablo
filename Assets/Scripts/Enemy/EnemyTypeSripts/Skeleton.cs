public class Skeleton : Enemy
{
    protected override void Start()
    {
        base.Start();
        stateController = GetComponent<StateController>();
        stateController.InitializeAI(true, waypoints);
    }

    public void TakePlayerDamage()
    {
        FieldOfView fov = stateController.GetComponent<FieldOfView>();
        if (fov == null) return;

        if (fov.DamageableTarget != null)
            EventsManager.OnPlayerApplyDamage?.Invoke(stateController.EnemyStats.Damage);
    }

    protected override void Die()
    {
        base.Die();
        int idQuest = 1;
        EventsManager.LocalQuestProgressIncreased?.Invoke(idQuest);
    }
}
