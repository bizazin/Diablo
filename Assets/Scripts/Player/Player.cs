using UnityEngine;
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerAnimationController animator;
    [SerializeField] private RestartGame restart;

    public PlayerStats PlayerStats;

    private JoystickForMovement joystick;
    private Movement movement;
    private int damageReducer;
    

    private void Start()
    {
        damageReducer = 300;
        joystick = GetComponent<JoystickForMovement>();
        movement = GetComponent<Movement>();
        TogglePlayer(true);
        PlayerStats.CurrentHealth = PlayerStats.MaxHealth;
        healthBar.SetMaxHealth(PlayerStats.MaxHealth);
        EventsManager.OnPlayerApplyDamage += ApplyDamage;
    }

    public void ApplyDamage(int damageValue)
    {
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
            TogglePlayer(false);
           // restart.ShowWindow();
            EventsManager.OnDeath.Invoke();
        }
    }

    private void TogglePlayer(bool state)
    {
        movement.enabled = state;
        joystick.enabled = state;
    }
}
