using UnityEngine;
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerAnimationController animator;

    public PlayerStats PlayerStats;

    private void Start()
    {
        PlayerStats.CurrentHealth = PlayerStats.MaxHealth;
        healthBar.SetMaxHealth(PlayerStats.MaxHealth);
        EventsManager.OnPlayerApplyDamage += ApplyDamage;
    }

    public void ApplyDamage(int damageValue)
    {
        int damageReducer = 300;
        damageValue -= damageValue * PlayerStats.Defence / damageReducer;
        PlayerStats.CurrentHealth -= damageValue;
        healthBar.SetHealth(PlayerStats.CurrentHealth);

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (PlayerStats.CurrentHealth <= 0)
        {
            animator.Die();
            EventsManager.OnDeath.Invoke();
        }
    }
}
