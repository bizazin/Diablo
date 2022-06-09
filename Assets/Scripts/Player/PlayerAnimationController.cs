
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    
    [Header("States")]


    public void Run()
    {
        animator.SetBool(run, true);
    }

    public void Idle()
    {
        animator.SetBool(idle,true);
    }

    public void Attack()
    {
string randomNumber = Random.Range(1, 4).ToString();
        if (animator.GetCurrentAnimatorStateInfo(1).IsName(attack))
        {
            animator.SetBool(run,false);
            animator.SetBool(idle,false);
            animator.SetTrigger(attack + randomNumber); 
        }
}
    }

    public void Die()
    {
        animator.SetBool(death,true);
    }
    
}
