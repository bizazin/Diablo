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
    }

    public void AnimateWalk(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", false);
    }
}
