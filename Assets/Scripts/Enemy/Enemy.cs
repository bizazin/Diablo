using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CurrentState
{
    Patrol = 0,
    Attack = 1
}

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Properties")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected List<Transform> waypoints;
    [SerializeField] protected ItemPickup itemToDrop;


    protected int currentHealth;
    protected EnemyHealthBar enemyHealthBar;
    protected EnemyAnimationController enemyAnimator;
    protected NavMeshAgent enemyAgent;
    protected StateController stateController;
    
    public EnemyStats EnemyStats;
    public CurrentState CurState;

    
    private Collider collider;

    protected virtual void Start()
    {
        maxHealth = EnemyStats.MaxHealth;
        enemyAnimator = GetComponent<EnemyAnimationController>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
        stateController = GetComponent<StateController>();
        collider = GetComponent<Collider>();

        currentHealth = maxHealth;
        enemyHealthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    protected virtual void Update()
    {
        SetEnemyAnimation(enemyAnimator);
    }

    protected void SetEnemyAnimation(EnemyAnimationController animationController)
    {
        if (Mathf.Abs(enemyAgent.velocity.x) > 0.2f || Mathf.Abs(enemyAgent.velocity.z) > 0.2f)
        {
            // if (CurState == CurrentState.Attack)
            //     animationController.AnimateRun(true);
            //
            // else if (CurState == CurrentState.Patrol)
            //     animationController.AnimateWalk(true);
            //
            switch(CurState)
            {
              case CurrentState.Attack:   
                  animationController.AnimateRun(true);
                  break;
              case CurrentState.Patrol:
                  animationController.AnimateWalk(true);
                  break;
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

    public IEnumerator ReducingDelay(int damageValue)
    {
        yield return new WaitForSeconds(.7f);
        currentHealth -= damageValue;
        enemyHealthBar.UpdateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        stateController.enabled = false;
        collider.enabled = false;
        enemyHealthBar.gameObject.SetActive(false);
        enemyAgent.enabled = false;
        enemyAnimator.AnimateDie(true);

        DropItem();
        StartCoroutine(DisappearDelay());
    }

    private void DropItem()
    {
        if (TryGetComponent(out DarkNight darkNight) || TryGetComponent(out Demon demon))
        {
            for (int i = 0; i < 3; i++)
            {
                var pos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                Instantiate(itemToDrop, pos, Quaternion.identity);
            }
        }
        else
        {
            var pos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            Instantiate(itemToDrop, pos, Quaternion.identity);
        }
    }

    private IEnumerator DisappearDelay()
    {
        yield return new WaitForSeconds(3f);

        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveY(transform.position.y - 5f, 10f));

        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
