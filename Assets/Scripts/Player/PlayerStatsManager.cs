using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]private EquipmentManager equipmentManager;
    
    [SerializeField] private Player player;
    private void Start()
    {
        EventsManager.OnStatsChanged += ChangeStats;
    }
    public void ChangeStats()
    {
        player.playerStats.Damage = 0;
        player.playerStats.Defence = 0;
        player.playerStats.CriticalChance = 0;
        player.playerStats.CriticalDamage = 0;
        player.playerStats.Speed = 0;
        foreach (var slot in equipmentManager.EquipmentSlots)
        {
            if (slot != null)
            {
                player.playerStats.Damage += slot.Stats.Damage;
                player.playerStats.Defence += slot.Stats.Defence;
                player.playerStats.CriticalChance += slot.Stats.CriticalChance;
                player.playerStats.CriticalDamage += slot.Stats.CriticalDamage;
                player.playerStats.Speed += slot.Stats.Speed;
            }
        }
    }
    private void OnDisable()
    {
        EventsManager.OnStatsChanged -= ChangeStats;
    }
}
