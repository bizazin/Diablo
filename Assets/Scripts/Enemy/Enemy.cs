using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public enum CurrentState
{
    Patrol, 
    Attack
}

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Properties")]
    [SerializeField] protected int maxHealth;

    public CurrentState currentState;
    protected int currentHealth;

    protected EnemyAnimationController enemyAnimator;
    protected NavMeshAgent enemyAgent;

    protected virtual void Start()
    {
        enemyAnimator = GetComponent<EnemyAnimationController>();
        enemyAgent = GetComponent<NavMeshAgent>();
        //EventsManager.OnEnemyDamageTaken += ApplyDamage;
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        SetEnemyAnimation(enemyAnimator);
    }

    protected void SetEnemyAnimation(EnemyAnimationController animationController)
    {
        if (Mathf.Abs(enemyAgent.velocity.x) > 0.2f || Mathf.Abs(enemyAgent.velocity.z) > 0.2f)
        {
            if (currentState == CurrentState.Attack)
            {
                animationController.AnimateRun(true);
            }
            else if (currentState == CurrentState.Patrol)
            {
                animationController.AnimateWalk(true);
            }
        }
        else
        {
            animationController.AnimateRun(false);
        }
    }

    public void ApplyDamage(int damageValue)
    {
        currentHealth -= damageValue;

        if (currentHealth < 0)
        {
            
        }
    }
}
