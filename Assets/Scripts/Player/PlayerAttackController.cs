using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController animator;
    [SerializeField] private Button attackButton;
    public int i;

    private void OnEnable()
    {
        attackButton.onClick.AddListener(Attack);
    }

    public void Attack()
    {
        animator.Attack();
        int value = Random.Range(1, 100);
        KeyManager.SetPrefsValue(KeyManager.Name,value);
    }
    
    private void OnDisable()
    {
        attackButton.onClick.RemoveListener(Attack);
    }


}
