using BattleDrakeStudios.ModularCharacters;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsUIManager : MonoBehaviour
{
    [SerializeField] private StatsUIComponent[] statsUIs;
    
    private EquipmentManager equipmentManager;
    private Dictionary<StatsType, float> defaultSlidersValues;

    private Equipment[] currentEquipment;

    private void OnEnable()
    {
        EventsManager.OnStatsUIChanged += ChangeStats;
        EventsManager.OnUnequippedOrDeletedUI += UnequipStats;
    }

    private void Start()
    {
        equipmentManager = GetComponentInParent<EquipmentManager>();
        currentEquipment = new Equipment[equipmentManager.EquipmentSlots.Length];
        defaultSlidersValues = new Dictionary<StatsType, float>();

        SetEquippedStuff();
        SetDefaultValues();
        SetSliders();
    }

    private void SetEquippedStuff()
    {
        Array.Copy(equipmentManager.EquipmentSlots, currentEquipment, currentEquipment.Length);
    }

    private void ChangeStats(InventorySlot selectedSlot)
    {
        ChangeSliders(selectedSlot);
//        ChangeDifferences();
    }


    private void ChangeSliders(InventorySlot selectedSlot)
    {
        if (selectedSlot != null)
        {
            var equipment = selectedSlot.Item as Equipment;
            SetEquippedStuff();
            AddToCurEquipment(equipment);
        }
        else SetEquippedStuff();
        SetSliders();
    }

    private void ChangeDifferences()
    {
        throw new NotImplementedException();
    }
    
    private void UnequipStats(Equipment equipment)
    {
        DeleteFromCurEquipment(equipment);
        SetSliders();
    }

    private void AddToCurEquipment(Equipment equipment)
    {
        var idEquip = (int)equipment.ArmorType;
        currentEquipment[idEquip] = equipment;
    }

    private void DeleteFromCurEquipment(Equipment equipment)
    {
        var idEquip = (int)equipment.ArmorType;
        currentEquipment[idEquip] = null;
    }

    private void SetDefaultValues()
    {
        for (int i = 0; i < statsUIs.Length; i++)
            defaultSlidersValues.Add((StatsType)i, statsUIs[i].Slider.value);
    }

    private void SetSliders()
    {
        SetSlidersToDefault();

        foreach (var item in currentEquipment)
            if (item != null)
            {
                var stats = item.Stats;
                foreach (var ui in statsUIs)
                {
                    if (ui.StatsType == StatsType.Speed)
                        ui.Slider.value += stats.Speed;

                    if (ui.StatsType == StatsType.Defence)
                        ui.Slider.value += stats.Defence;

                    if (ui.StatsType == StatsType.CriticalChance)
                        ui.Slider.value += stats.CriticalChance;

                    if (ui.StatsType == StatsType.CriticalDamage)
                        ui.Slider.value += stats.CriticalDamage;

                    if (ui.StatsType == StatsType.Damage)
                        ui.Slider.value += stats.Damage;
                }
            }
    }

    private void SetSlidersToDefault()
    {
/*        foreach (var ui in statsUIs)
        {
            if (ui.StatsType == StatsType.Speed)
                ui.Slider.value = defaultSlidersValues[StatsType.Speed];

            if (ui.StatsType == StatsType.Defence)
                ui.Slider.value = defaultSlidersValues[StatsType.Defence];

            if (ui.StatsType == StatsType.CriticalChance)
                ui.Slider.value = defaultSlidersValues[StatsType.CriticalChance];

            if (ui.StatsType == StatsType.CriticalDamage)
                ui.Slider.value = defaultSlidersValues[StatsType.CriticalDamage];

            if (ui.StatsType == StatsType.Damage)
                ui.Slider.value = defaultSlidersValues[StatsType.Damage];
        }*/

        for (int i = 0; i < statsUIs.Length; i++)
            statsUIs[i].Slider.value = defaultSlidersValues[(StatsType)i];
    }

    private void OnDisable()
    {
        EventsManager.OnStatsUIChanged -= ChangeStats;
        EventsManager.OnUnequippedOrDeletedUI -= UnequipStats;
    }
}