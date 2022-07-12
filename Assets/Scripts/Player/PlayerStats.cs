using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats")]
public class PlayerStats: ScriptableObject
{
    public int MaxHealth;
    public int CurrentHealth;
    public int Damage;
    public int Defence;
    public int CriticalChance;//percent
    public int CriticalDamage;//percent
    public int Speed;

}
