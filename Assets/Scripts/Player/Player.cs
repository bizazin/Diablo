using UnityEngine;


public class Player : MonoBehaviour, IDamageable
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
        EventsManager.OnPlayerApplyDamage += TakeDamage;
    }

    private void OnDisable()
    {
        EventsManager.OnPlayerApplyDamage -= TakeDamage;
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

    public void ApplyDamage(int damageValue)
    {
        playerStats.CurrentHealth -= damageValue;
        _healthBar.SetHealth(playerStats.MaxHealth);
        if (playerStats.CurrentHealth <= 0)
        {
            animator.Die();
            EventsManager.OnDeath.Invoke();
        }
    }
}
