using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    #region Singleton

    public static PlayerStatsManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }
    #endregion

    [SerializeField] private Player player;

    private EquipmentManager equipmentManager;

    private void Start()
    {
        equipmentManager = GetComponentInParent<EquipmentManager>();
        EventsManager.OnStatsChanged += ChangeStats;
    }

    public void ChangeStats()
    {
        ResetStats();

        foreach (var slot in equipmentManager.EquipmentSlots)
            if (slot != null)
            {
                player.PlayerStats.Damage += slot.Stats.Damage;
                player.PlayerStats.Defence += slot.Stats.Defence;
                player.PlayerStats.CriticalChance += slot.Stats.CriticalChance;
                player.PlayerStats.CriticalDamage += slot.Stats.CriticalDamage;
                player.PlayerStats.Speed += slot.Stats.Speed;
            }
    }

    private void ResetStats()
    {
        player.PlayerStats.Damage = 0;
        player.PlayerStats.Defence = 0;
        player.PlayerStats.CriticalChance = 0;
        player.PlayerStats.CriticalDamage = 0;
        player.PlayerStats.Speed = 0;
    }
    private void OnDisable()
    {
        EventsManager.OnStatsChanged -= ChangeStats;
    }
}
