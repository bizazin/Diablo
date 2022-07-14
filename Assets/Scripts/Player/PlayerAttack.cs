using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController animator;
    [SerializeField] private Button attackButton;
    [SerializeField] private PlayerFieldOfView fovPlayer;
    private bool canAttack =true;
 
    private void OnEnable()
    {
        attackButton.onClick.AddListener(Attack);
        //player = GetComponent<Player>();
    }

    public void Attack()
    {
        animator.Attack();
       // StartCoroutine(CanAttack());
       if (canAttack && fovPlayer.damageableTargets.Count > 0)
       {

           foreach (var enemy in fovPlayer.damageableTargets)
               enemy.GetComponent<Enemy>().ApplyDamage(1);
           
           StartCoroutine(CanAttack());
       }


      
        //  player.Attack();
    }
 

    IEnumerator CanAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
    

    private void OnDisable()
    {
        attackButton.onClick.RemoveListener(Attack);
    }
}
