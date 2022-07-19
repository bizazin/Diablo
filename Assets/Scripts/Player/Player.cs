using UnityEngine;


public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PlayerAnimationController animator;
    public PlayerStats playerStats;
    
    private void Start()
    {
        playerStats.CurrentHealth = playerStats.MaxHealth;
        _healthBar.SetMaxHealth(playerStats.MaxHealth);
        EventsManager.OnPlayerApplyDamage += ApplyDamage;
    }
    
    public void ApplyDamage(int damageValue)
    {
        int damageReducer = 300;
        damageValue -= damageValue * playerStats.Defence/damageReducer;
        playerStats.CurrentHealth -= damageValue;
        _healthBar.SetHealth(playerStats.CurrentHealth);
        if (playerStats.CurrentHealth <= 0)
        {
            animator.Die();
            EventsManager.OnDeath.Invoke();
        }
    }
    
}
