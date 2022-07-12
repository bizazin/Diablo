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
        itemToDrop.Stats = stats;
        return itemToDrop;
    }

    private void ChooseItemType()
    {
        int idType = Random.Range(0,5);
        switch (idType)
        {
            case 0: 
                currentItemID = Random.Range(0,container.weapon.Length);
                itemToDrop = container.weapon[currentItemID];
                break;
            case 1:
                currentItemID = Random.Range(0,container.helmet.Length);
                itemToDrop = container.helmet[currentItemID];
                break;
            case 2:
                currentItemID = Random.Range(0,container.chest.Length);
                itemToDrop = container.chest[currentItemID];
                break;
            case 3:
                currentItemID = Random.Range(0,container.arms.Length);
                itemToDrop = container.arms[currentItemID];
                break;
            case 4:
                currentItemID = Random.Range(0,container.legs.Length);
                itemToDrop = container.legs[currentItemID];
                break;
        }
    }
    private void SetRare()
    {
        int randomValue = Random.Range(0,100);
        Vector2Int[] chance = statsValues.rarityChanceRange;
        ItemStats.Rarity[] rarity = (ItemStats.Rarity[]) Enum.GetValues(typeof(ItemStats.Rarity));
        for (int i = 0; i< rarity.Length;i++)
            if (randomValue >= chance[i].x && randomValue <=chance[i].y )
                stats.rarity = rarity[i];
    }

    private void RandomStats()
    {
        int statsID = (int)stats.rarity;
        Vector2Int valuesRange = statsValues.statValuesRange[statsID];
        Vector2Int critRange = statsValues.criticalChanceRange[statsID];
        SetDamage(valuesRange.x,valuesRange.y);
        SetDefence(valuesRange.x,valuesRange.y);
        SetCriticalDamage(valuesRange.x,valuesRange.y);
        SetSpeed(valuesRange.x,valuesRange.y);
        SetCriticalChance(critRange.x,critRange.y);
        
        MultiplyStats(statsID);
    }
    
    private void SetDamage(int min, int max)
    {
        stats.damage = Random.Range(min, max);
    }
    private void SetDefence(int min, int max)
    {
        stats.defence = Random.Range(min, max);
    } 
    private void SetCriticalDamage(int min, int max)
    {
        stats.criticalDamage = Random.Range(min, max);
    }
    private void SetCriticalChance(int min, int max)
    {
        stats.criticalChance = Random.Range(min, max);
    }
    private void SetSpeed(int min, int max)
    {
        stats.speed = Random.Range(min, max);
    }
    private void MultiplyStats(int idStats)
    {
        stats.damage = (int)(stats.damage * statsValues.statsMultiplier[idStats]);
        stats.defence = (int)(stats.defence * statsValues.statsMultiplier[idStats]);
        stats.criticalDamage = (int)(stats.criticalDamage * statsValues.statsMultiplier[idStats]);
        stats.criticalChance = (int)(stats.criticalChance * statsValues.statsMultiplier[idStats]);
        stats.speed = (int)(stats.criticalChance * statsValues.statsMultiplier[idStats]);
    }

    private void ClearItemSlots()
    {
        stats = null;
        itemToDrop = null;
    }
    
    
}
