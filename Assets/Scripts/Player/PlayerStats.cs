using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int MaxHealth;
    public int CurrentHealth;
    public int Speed;
    public int Damage;
    public int Defence;
    public int CriticalChance;//percent
    public int CriticalDamage;//percent

    public void ResetStats()
    {
        Speed = 4;
        Damage = 5;
        Defence = 5;
        CriticalChance = 0;
        CriticalDamage = 0;
    }
}

public enum StatsType
{
    Speed = 0,
    Damage = 1,
    Defence = 2,
    CriticalChance = 3,
    CriticalDamage = 4
}
