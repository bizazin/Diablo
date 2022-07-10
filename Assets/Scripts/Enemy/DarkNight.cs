using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkNight : Enemy
{
    private StateController stateController;

    protected override void Start()
    {
        base.Start();
        stateController = GetComponent<StateController>();
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
