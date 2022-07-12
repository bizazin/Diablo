using UnityEngine;

public class Player : MonoBehaviour
{
   // [SerializeField] private int _maxHealth = 100;
   // [SerializeField] private int _currentHealth;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PlayerAnimationController animator;
    public PlayerStats playerStats;
    
    private void Start()
    {
        playerStats.CurrentHealth = playerStats.MaxHealth;
        _healthBar.SetMaxHealth(playerStats.MaxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(20);
    }

    private void TakeDamage(int damage)
    {
        playerStats.CurrentHealth -= damage;
        _healthBar.SetHealth(playerStats.CurrentHealth);
        if (playerStats.CurrentHealth<=0)
        {
            animator.Die();
            EventsManager.OnDeath.Invoke();
        }
    }
}
