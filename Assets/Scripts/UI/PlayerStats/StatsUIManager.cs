using BattleDrakeStudios.ModularCharacters;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsUIManager : MonoBehaviour
{
    [SerializeField] private StatsUIComponent[] statsUIs;

    private Dictionary<StatsType, float> defaultSlidersValues;
    private Dictionary<StatsType, float> currentSlidersValues;

    private Equipment[] currentEquipment;

    private void OnEnable()
    {
        EventsManager.OnStatsUIChanged += ChangeStats;
    }

    private void Start()
    {
        currentEquipment = new Equipment[Enum.GetNames(typeof(EquipmentType)).Length];
        CheckCurrentEquipment();
        defaultSlidersValues = new Dictionary<StatsType, float>();
        currentSlidersValues = new Dictionary<StatsType, float>();
        SetDefaultValues();
        SetCurrentValues();
    }

    private void CheckCurrentEquipment()
    {
        var equipManager = new EquipmentManager();
        equipManager.EquipmentSlots.CopyTo(currentEquipment, 0);
    }

    private void ChangeStats(InventorySlot selectedSlot)
    {
        if (selectedSlot != null)
        {
            var equipment = selectedSlot.Item as Equipment;
            AddToEquipment(equipment);
            SetSliders(equipment);
        }
    }

    private void AddToEquipment(Equipment equipment)
    {
        var idEquip = (int)equipment.ArmorType;
        currentEquipment[idEquip] = equipment;
    }

    private void SetDefaultValues()
    {
        foreach (var item in statsUIs)
        {
            if (item.StatsType == StatsType.Speed)
                defaultSlidersValues.Add(StatsType.Speed, item.Slider.value);

            if (item.StatsType == StatsType.Defence)
                defaultSlidersValues.Add(StatsType.Defence, item.Slider.value);

            if (item.StatsType == StatsType.CriticalChance)
                defaultSlidersValues.Add(StatsType.CriticalChance, item.Slider.value);

            if (item.StatsType == StatsType.CriticalDamage)
                defaultSlidersValues.Add(StatsType.CriticalDamage, item.Slider.value);

            if (item.StatsType == StatsType.Damage)
                defaultSlidersValues.Add(StatsType.Damage, item.Slider.value);
        }
    }

    private void SetCurrentValues()
    {
        foreach (var item in statsUIs)
        {
            if (item.StatsType == StatsType.Speed)
                currentSlidersValues.Add(StatsType.Speed, item.Slider.value);

            if (item.StatsType == StatsType.Defence)
                currentSlidersValues.Add(StatsType.Defence, item.Slider.value);

            if (item.StatsType == StatsType.CriticalChance)
                currentSlidersValues.Add(StatsType.CriticalChance, item.Slider.value);

            if (item.StatsType == StatsType.CriticalDamage)
                currentSlidersValues.Add(StatsType.CriticalDamage, item.Slider.value);

            if (item.StatsType == StatsType.Damage)
                currentSlidersValues.Add(StatsType.Damage, item.Slider.value);
        }
    }


    private void SetSliders(Equipment equipment)
    {
        var stats = equipment.Stats;
        foreach (var item in statsUIs)
        {
            if (item.StatsType == StatsType.Speed)
                item.Slider.value = defaultSlidersValues[StatsType.Speed] + stats.Speed;

            if (item.StatsType == StatsType.Defence)
                item.Slider.value = defaultSlidersValues[StatsType.Defence] + stats.Defence;

            if (item.StatsType == StatsType.CriticalChance)
                item.Slider.value = defaultSlidersValues[StatsType.CriticalChance] + stats.CriticalChance;

            if (item.StatsType == StatsType.CriticalDamage)
                item.Slider.value = defaultSlidersValues[StatsType.Defence] + stats.CriticalDamage;

            if (item.StatsType == StatsType.Damage)
                item.Slider.value = defaultSlidersValues[StatsType.Damage] + stats.Damage;
        }
    }

    public void SetSlidersToDefault()
    {
        foreach (var item in statsUIs)
        {
            if (item.StatsType == StatsType.Speed)
                item.Slider.value = defaultSlidersValues[StatsType.Speed];
        
            if (item.StatsType == StatsType.CriticalDamage)
                item.Slider.value = defaultSlidersValues[StatsType.CriticalDamage];
        
            if (item.StatsType == StatsType.CriticalDamage)
                item.Slider.value = defaultSlidersValues[StatsType.CriticalDamage];
        
            if (item.StatsType == StatsType.CriticalDamage)
                item.Slider.value = defaultSlidersValues[StatsType.CriticalDamage];
        
            if (item.StatsType == StatsType.Damage)
                item.Slider.value = defaultSlidersValues[StatsType.Damage];
        }
    }

    private void SetSlidersToCurrent()
    {
        foreach (var item in statsUIs)
        {
            if (item.StatsType == StatsType.Speed)
                item.Slider.value = currentSlidersValues[StatsType.Speed];
        
            if (item.StatsType == StatsType.CriticalDamage)
                item.Slider.value = currentSlidersValues[StatsType.CriticalDamage];
        
            if (item.StatsType == StatsType.CriticalDamage)
                item.Slider.value = currentSlidersValues[StatsType.CriticalDamage];
        
            if (item.StatsType == StatsType.CriticalDamage)
                item.Slider.value = currentSlidersValues[StatsType.CriticalDamage];
        
            if (item.StatsType == StatsType.Damage)
                item.Slider.value = currentSlidersValues[StatsType.Damage];
        }
    }
    private void OnDisable()
    {
        EventsManager.OnStatsUIChanged -= ChangeStats;
    }
}