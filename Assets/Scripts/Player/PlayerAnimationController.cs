
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    
    [Header("States")]
    [SerializeField] private string _run = "Run";
    [SerializeField] private string _idle = "Idle";
    [SerializeField] private string _death = "Death";
    [SerializeField] private string _attack = "Attack";

    public void Run()
    {
        animator.SetBool(_idle, false);
        animator.SetBool(_run, true);
    }

    public void Idle()
    {
        animator.SetBool(_run, false);
        animator.SetBool(_idle, true);
    }

    public void Attack()
    {
        string randomNumber = Random.Range(1, 4).ToString();
        Debug.Log("layerIndex: " + animator.GetLayerIndex(_attack));
            animator.SetBool(_run, false);
            animator.SetBool(_idle, false);
            animator.SetTrigger(_attack + randomNumber);
    }

    public void Die()
    {
        animator.SetBool(_run, false);
        animator.SetBool(_idle, false);
        animator.SetBool(_attack, false);
        animator.SetBool(_death, true);
    }
    
}
