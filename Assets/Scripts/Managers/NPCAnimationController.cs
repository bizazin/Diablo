using UnityEngine;

public class NpcAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("States")]
    [SerializeField] private string idle = "Idle";
    [SerializeField] private string talk = "Talk";

    public void Talk()
    {
        animator.SetBool(idle, false);
        animator.SetBool(talk, true);
    }

    public void Idle()
    {
        animator.SetBool(talk, false);
        animator.SetBool(idle, true);
    }
}
