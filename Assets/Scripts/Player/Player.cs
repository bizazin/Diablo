using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PlayerAnimationController animator;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            TakeDamage(20);
    }

    private void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.SetHealth(_currentHealth);
        if (_currentHealth<=0)
        {
            animator.Die();
            EventsManager.OnDeath.Invoke();
        }
    }
}
