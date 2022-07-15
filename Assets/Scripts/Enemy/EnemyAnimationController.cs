using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        animator.SetFloat("IdleIndex", Random.Range(0f, 1f));
    }

    public void AnimateRun(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
    }

    public void AnimateWalk(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", false);
    }

    public void AnimateAttack(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }

    public void AnimateDamage(bool isDamage)
    {
        animator.SetBool("isDamage" , isDamage);
        StartCoroutine(DamageRecount());
    }

    public void AnimateDie(bool isDie)
    {
        animator.SetBool("Die" , isDie);
    }

    private IEnumerator DamageRecount()
    {
        yield return new WaitForSeconds(1f);

        AnimateDamage(false);
    }
}
