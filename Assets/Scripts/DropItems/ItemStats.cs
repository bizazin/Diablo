using System;

[Serializable]
public class ItemStats
{
    public int Damage;
    public int Defence;
    public int CriticalDamage;
    public int CriticalChance;
    public int Speed;
    public Rarity Rar;

    public enum Rarity
    {
        Common = 0,
        UnCommon = 1,
        Rare = 2,
        Epic = 3,
        Legendary = 4,
    }
}
