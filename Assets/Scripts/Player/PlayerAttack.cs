using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController animator;
    [SerializeField] private Button attackButton;
    [SerializeField] private PlayerFieldOfView fovPlayer;

    private bool canAttack = true;

    private void OnEnable()
    {
        attackButton.onClick.AddListener(Attack);
    }

    private void Attack()
    {
        animator.Attack();
        if (canAttack && fovPlayer.DamageableTargets.Count > 0)
        {
            foreach (var enemy in fovPlayer.DamageableTargets)
                enemy.GetComponent<Enemy>().ApplyDamage(SetPlayerDamage());
            StartCoroutine(CanAttack());
        }
    }

    private int SetPlayerDamage()
    {
        var stats = GetComponent<Player>().PlayerStats;
        int random = Random.Range(1, 101);

        if (random > stats.CriticalChance)
            return stats.Damage;

        return stats.Damage * (1 + stats.CriticalDamage / 100);
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
