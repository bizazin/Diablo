using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropManager : MonoBehaviour
{
    #region Singleton

    public static DropManager Instance;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }
    #endregion

    [SerializeField] private ItemsContainer container;
    [SerializeField] private Item itemToDrop;
    [SerializeField] private ItemStats stats;
    [SerializeField] private StatsValuesContainer statsValues;

    private int currentItemID;
    
    public Item SetupItem()
    {
        ClearItemSlots();
        stats = new ItemStats();
        ChooseItemType();
        
        SetRare();
        RandomStats();
        MultiplyStats(currentItemID);
        itemToDrop.IsNew = true;
        itemToDrop.Stats = stats;
        return itemToDrop;
    }

    private void ChooseItemType()
    {
        int idType = Random.Range(0, 5);
        switch (idType)
        {
            case 0: 
                currentItemID = Random.Range(0, container.Weapon.Length);
                itemToDrop = Instantiate(container.Weapon[currentItemID]);
                break;
            case 1:
                currentItemID = Random.Range(0, container.Helmet.Length);
                itemToDrop = Instantiate(container.Helmet[currentItemID]);
                break;
            case 2:
                currentItemID = Random.Range(0, container.Chest.Length);
                itemToDrop = Instantiate(container.Chest[currentItemID]);
                break;
            case 3:
                currentItemID = Random.Range(0, container.Arms.Length);
                itemToDrop = Instantiate(container.Arms[currentItemID]);
                break;
            case 4:
                currentItemID = Random.Range(0, container.Legs.Length);
                itemToDrop = Instantiate(container.Legs[currentItemID]);
                break;
        }
    }

    private void SetRare()
    {
        int random = Random.Range(0, 100);
        Vector2Int[] chance = statsValues.RarityChanceRange;
        ItemStats.Rarity[] rarity = (ItemStats.Rarity[]) Enum.GetValues(typeof(ItemStats.Rarity));

        for (int i = 0; i < rarity.Length; i++)
            if (random >= chance[i].x && random <= chance[i].y)
                stats.Rar = rarity[i];
    }

    private void RandomStats()
    {
        int statsID = (int)stats.Rar;
        Vector2Int critRange = statsValues.CriticalChanceRange[statsID];
        Vector2Int critDamage = statsValues.CriticalDamageRange[statsID];
        Vector2Int defence = statsValues.DefenceRange[statsID];
        Vector2Int speed = statsValues.SpeedRange[statsID];
        Vector2Int damage = statsValues.DamageRange[statsID];
        
        SetDamage(damage.x, damage.y);
        SetDefence(defence.x, defence.y);
        SetSpeed(speed.x, speed.y);
        SetCriticalChance(critRange.x, critRange.y);
        SetCriticalDamage(critDamage.x, critDamage.y);
        
        MultiplyStats(statsID);
    }
    
    private void SetDamage(int min, int max)
    {
        stats.Damage = Random.Range(min, max);
    }

    private void SetDefence(int min, int max)
    {
        stats.Defence = Random.Range(min, max);
    } 

    private void SetCriticalDamage(int min, int max)
    {
        stats.CriticalDamage = Random.Range(min, max);
    }

    private void SetCriticalChance(int min, int max)
    {
        stats.CriticalChance = Random.Range(min, max);
    }

    private void SetSpeed(int min, int max)
    {
        stats.Speed = Random.Range(min, max);
    }

    private void MultiplyStats(int idStats)
    {
        stats.Damage = (int)(stats.Damage * statsValues.StatsMultiplier[idStats]);
        stats.Defence = (int)(stats.Defence * statsValues.StatsMultiplier[idStats]);
        stats.CriticalDamage = (int)(stats.CriticalDamage * statsValues.StatsMultiplier[idStats]);
        stats.CriticalChance = (int)(stats.CriticalChance * statsValues.StatsMultiplier[idStats]);
        stats.Speed = (int)(stats.CriticalChance * statsValues.StatsMultiplier[idStats]);
    }

    private void ClearItemSlots()
    {
        stats = null;
        itemToDrop = null;
    }
}
