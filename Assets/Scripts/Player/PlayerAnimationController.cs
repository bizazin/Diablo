using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("States")]
    [SerializeField] private string run = "Run";
    [SerializeField] private string idle = "Idle";
    [SerializeField] private string death = "Death";
    [SerializeField] private string attack = "Attack";

    public void Run()
    {
        animator.SetBool(idle, false);
        animator.SetBool(run, true);
    }

    public void Idle()
    {
        animator.SetBool(run, false);
        animator.SetBool(idle, true);
    }

    public void Attack()
    {
        string randomNumber = Random.Range(1, 3).ToString();

        Debug.Log("layerIndex: " + animator.GetLayerIndex(attack));
        Debug.Log(animator.GetCurrentAnimatorStateInfo(1).IsName(attack));

        if (animator.GetCurrentAnimatorStateInfo(1).IsName(attack))
        {
            animator.SetBool(run, false);
            animator.SetBool(idle, false);
            animator.SetTrigger(attack + randomNumber);
        }
    }

    public void Die()
    {
        animator.SetBool(run, false);
        animator.SetBool(idle, false);
        animator.SetBool(attack, false);
        animator.SetBool(death, true);
    }
}
