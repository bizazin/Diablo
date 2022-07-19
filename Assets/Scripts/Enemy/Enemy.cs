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
    [SerializeField] protected ItemPickup itemToDrop;
    public EnemyStats EnemyStats;

    protected int currentHealth;

    protected EnemyHealthBar enemyHealthBar;
    protected EnemyAnimationController enemyAnimator;
    protected NavMeshAgent enemyAgent;

    public CurrentState currentState;

    protected virtual void Start()
    {
        maxHealth = EnemyStats.MaxHealth;
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

     public IEnumerator ReducingDelay(int damageValue)
    {
        yield return new WaitForSeconds(.7f);
        currentHealth -= damageValue;
        enemyHealthBar.UpdateHealthBar(maxHealth,currentHealth);
        if (currentHealth <= 0)
            Die();
    }

   protected virtual void Die()
    {
        //тут смэрть
        gameObject.GetComponent<StateController>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        enemyHealthBar.gameObject.SetActive(false);
        enemyAgent.enabled = false;
        enemyAnimator.AnimateDie(true);
        DropItem();
        StartCoroutine(DisappearDelay());
    }
   
   private void DropItem()
   {
       if (TryGetComponent(out DarkNight darkNight)||TryGetComponent(out Demon demon))
       {
           Instantiate(itemToDrop, new Vector3(transform.position.x,transform.position.y+1f, transform.position.z), Quaternion.identity);
           Instantiate(itemToDrop, new Vector3(transform.position.x,transform.position.y+1f, transform.position.z), Quaternion.identity);
           Instantiate(itemToDrop, new Vector3(transform.position.x,transform.position.y+1f, transform.position.z), Quaternion.identity);
       }
       else
       {
           Instantiate(itemToDrop, new Vector3(transform.position.x,transform.position.y+1f, transform.position.z), Quaternion.identity);
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
