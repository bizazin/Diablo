using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController animator;
    [SerializeField] private Button attackButton;

    private void OnEnable()
    {
        attackButton.onClick.AddListener(Attack);
    }

    public void Attack()
    {
        animator.Attack();
    }
    
    private void OnDisable()
    {
        attackButton.onClick.RemoveListener(Attack);
    }


}
