using System;
using UnityEngine;

[Serializable]
public class ItemStats 
{
    public int damage;
    public int defence;
    public int criticalDamage;
    public int criticalChance;
    public int speed;
    public Rarity rarity;
    public enum Rarity
    {
        Common = 0,
        UnCommon = 1,
        Rare = 2,
        Epic = 3,
        Legendary = 4,
    }
}
