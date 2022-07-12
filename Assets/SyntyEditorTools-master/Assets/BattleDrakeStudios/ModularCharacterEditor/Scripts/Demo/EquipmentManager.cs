using System;
using BattleDrakeStudios.ModularCharacters;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using bizazin;

public class EquipmentManager : MonoBehaviour
{
    public Equipment[] equipmentSlots;
    [SerializeField] private ModularCharacterManager characterManager;
    private SaveManager save;

    private void OnEnable()
    {
        EventsManager.OnItemPickedUp += OnEquipmentAdded;
        EventsManager.OnItemEquipped += EquipItem;
        EventsManager.OnItemUnequipped += Unequip;
    }

    private void Start()
    {
        
        // save = GetComponent<SaveManager>();
        // if (rem.GetConfig(RemoteConfigs.EnableCustomInventory).Value == "1")
        // {
        //     equipmentSlots = JsonConvert.DeserializeObject<Equipment[]>(rem.GetConfig(RemoteConfigs.Inventory).DefaultValue);
        // }
        // else
        // {
        //     equipmentSlots = save.LoadJsonArray("EquipmentList", equipmentSlots);
        // }


        foreach (var item in equipmentSlots)
        {
            if (item != null)
                EquipItem(item);
        }
    }

    public void EquipItem(Equipment equipment)
    {
        foreach (var part in equipment.ArmorParts)
        {
            if (part.partID > -1)
                characterManager.ActivatePart(part.bodyType, part.partID);
            else
                characterManager.DeactivatePart(part.bodyType);
        }
        
        int idEquip = (int)equipment.ArmorType;
        equipmentSlots[idEquip] = equipment;
        
        EventsManager.OnStatsChanged.Invoke();
    }

    public void Unequip(Equipment unequip)
    {
        foreach (var part in unequip.ArmorParts)
            characterManager.ActivatePart(part.bodyType, 0);
        EventsManager.OnStatsChanged.Invoke();
    }

    private void OnEquipmentAdded(Equipment itemToPickedUp)
    {
        int i = (int)itemToPickedUp.ArmorType;
        equipmentSlots[i] = itemToPickedUp;
        EquipItem(itemToPickedUp);
    }

    private void OnDisable()
    {
        EventsManager.OnItemPickedUp -= OnEquipmentAdded;
        EventsManager.OnItemEquipped -= EquipItem;
       // save.SaveToFile("EquipmentList", equipmentSlots);
    }

}

