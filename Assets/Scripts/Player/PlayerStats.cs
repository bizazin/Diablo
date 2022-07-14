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

}

public enum StatsType
{
    MaxHealth,
    Speed,
    Damage,
    Defence,
    CriticalChance,
    CriticalDamage
}
