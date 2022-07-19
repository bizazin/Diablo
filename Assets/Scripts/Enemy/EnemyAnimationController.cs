using System.Collections;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [HideInInspector] public Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();

        Anim.SetFloat("IdleIndex", Random.Range(0f, 1f));
    }

    public void AnimateRun(bool isRunning)
    {
        Anim.SetBool("isRunning", isRunning);
        Anim.SetBool("isWalking", false);
        Anim.SetBool("isAttacking", false);
    }

    public void AnimateWalk(bool isWalking)
    {
        Anim.SetBool("isWalking", isWalking);
        Anim.SetBool("isRunning", false);
    }

    public void AnimateAttack(bool isAttacking)
    {
        Anim.SetBool("isAttacking", isAttacking);
    }

    public void AnimateDamage(bool isDamage)
    {
        Anim.SetBool("isDamage", isDamage);
        StartCoroutine(DamageRecount());
    }

    public void AnimateDie(bool isDie)
    {
        Anim.SetBool("Die", isDie);
    }

    private IEnumerator DamageRecount()
    {
        yield return new WaitForSeconds(1f);

        AnimateDamage(false);
    }
}
