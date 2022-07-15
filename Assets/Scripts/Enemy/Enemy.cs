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
    [SerializeField] protected List<Transform> waypoints;

    protected int currentHealth;

    protected EnemyHealthBar enemyHealthBar;
    protected EnemyAnimationController enemyAnimator;
    protected NavMeshAgent enemyAgent;

    public CurrentState currentState;

    protected virtual void Start()
    {
        enemyAnimator = GetComponent<EnemyAnimationController>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();

        currentHealth = maxHealth;
        enemyHealthBar.UpdateHealthBar(maxHealth,currentHealth);
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
            animationController.AnimateRun(false);
    }

    public void ApplyDamage(int damageValue)
    {
        StartCoroutine(ReducingDelay(damageValue));
        enemyAnimator.AnimateDamage(true);

    }
    IEnumerator ReducingDelay(int damageValue)
    {
        yield return new WaitForSeconds(.7f);
        currentHealth -= damageValue;
        enemyHealthBar.UpdateHealthBar(maxHealth,currentHealth);
        if (currentHealth == 0)
        {
            //тут смэрть
            gameObject.GetComponent<StateController>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            enemyHealthBar.gameObject.SetActive(false);
            enemyAgent.enabled = false;
            enemyAnimator.AnimateDie(true);
            StartCoroutine(DisappearDelay());
        }
    }

    private IEnumerator DisappearDelay()
    {
        yield return new WaitForSeconds(3f);

        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveY(transform.position.y - 5f , 10f));

        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
