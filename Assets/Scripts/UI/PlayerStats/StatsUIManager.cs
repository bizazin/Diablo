using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsUIManager : MonoBehaviour
{
    [SerializeField] private StatsUIComponent[] statsUIs;

    private EquipmentManager equipmentManager;
    private Dictionary<StatsType, int> defaultSlidersValues;

    private Equipment[] predictableEquipment;

    private void OnEnable()
    {
        EventsManager.OnStatsUIChanged += ChangeStats;
        EventsManager.OnUnequippedOrDeletedUI += UnequipStats;
    }

    private void Start()
    {
        equipmentManager = GetComponentInParent<EquipmentManager>();
        predictableEquipment = new Equipment[equipmentManager.EquipmentSlots.Length];
        defaultSlidersValues = new Dictionary<StatsType, int>();

        SetEquippedStuff();
        SetDefaultValues();
        SetSliders();
    }

    private void SetEquippedStuff()
    {
        Array.Copy(equipmentManager.EquipmentSlots, predictableEquipment, predictableEquipment.Length);
    }

    private void SetDefaultValues()
    {
        for (int i = 0; i < statsUIs.Length; i++)
            defaultSlidersValues.Add((StatsType)i, (int)statsUIs[i].Slider.value);
    }

    private void SetSliders()
    {
        SetSlidersToDefault();

        foreach (var item in predictableEquipment)
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
        for (int i = 0; i < statsUIs.Length; i++)
            statsUIs[i].Slider.value = defaultSlidersValues[(StatsType)i];
    }

    private void ChangeStats(InventorySlot selectedSlot)
    {
        SetPredEquipment(selectedSlot);

        SetSliders();
        SetTextAsToSliders();
    }

    private void SetPredEquipment(InventorySlot selectedSlot)
    {
        if (selectedSlot != null)
        {
            var equipment = selectedSlot.Item as Equipment;
            SetEquippedStuff();
            AddToCurEquipment(equipment);
        }
        else SetEquippedStuff();
    }

    private void AddToCurEquipment(Equipment equipment)
    {
        var idEquip = (int)equipment.ArmorType;
        predictableEquipment[idEquip] = equipment;
    }

    private void SetTextAsToSliders()
    {
        SetTextValuesToDef();

        foreach (var equip in predictableEquipment)
            if (equip != null)
                foreach (var statUI in statsUIs)
                {
                    statUI.TextValue.text = statUI.Slider.value.ToString();

                    if (statUI.StatsType == StatsType.CriticalChance || statUI.StatsType == StatsType.CriticalDamage)
                        statUI.TextValue.text += " %";
                }
    }

    private void SetTextValuesToDef()
    {
        for (int i = 0; i < statsUIs.Length; i++)
        {
            statsUIs[i].TextValue.text = defaultSlidersValues[(StatsType)i].ToString();

            if (statsUIs[i].StatsType == StatsType.CriticalChance || statsUIs[i].StatsType == StatsType.CriticalDamage)
                statsUIs[i].TextValue.text += " %";
        }
    }

    private void UnequipStats(Equipment equipment)
    {
        DeleteFromCurEquipment(equipment);
        SetSliders();
        SetTextAsToSliders();
    }

    private void DeleteFromCurEquipment(Equipment equipment)
    {
        var idEquip = (int)equipment.ArmorType;
        predictableEquipment[idEquip] = null;
    }

    private void OnDisable()
    {
        EventsManager.OnStatsUIChanged -= ChangeStats;
        EventsManager.OnUnequippedOrDeletedUI -= UnequipStats;
    }
}