public class Skeleton : Enemy
{
    private StateController stateController;

    protected override void Start()
    {
        base.Start();
        stateController = GetComponent<StateController>();
        stateController.InitializeAI(true, this.waypoints);
    }

    public void TakePlayerDamage()
    {
        FieldOfView fov = stateController.GetComponent<FieldOfView>();
        if (fov == null) return;

        if (fov.damageableTarget != null)
        {
            EventsManager.OnPlayerApplyDamage?.Invoke(stateController.EnemyStats.Damage);
        }
    }
}
