using System;
using BattleDrakeStudios.ModularCharacters;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    [SerializeField] private Item[] equipmentSlots;

    [SerializeField] private ModularCharacterManager characterManager;

    [SerializeField] private RemoteConfigStorage rem;
    private SaveManager save;
    

    private void Start()
    {
        
        save = GetComponent<SaveManager>();
        if (rem.GetConfig(RemoteConfigs.EnableCustomInventory).Value == "1")
        {
            equipmentSlots = JsonConvert.DeserializeObject<Item[]>(rem.GetConfig(RemoteConfigs.Inventory).DefaultValue);
        }
        else
        {
            equipmentSlots = save.LoadJsonArray("EquipmentList", equipmentSlots);
        }
        
        EventsManager.OnItemPickedUp+= OnItemAdded;

        foreach (var item in equipmentSlots)
        {
            if (item!=null)
            {
                EquipItem(item);
            }
        }
    }

    private void EquipItem(Item itemToEquip)
    {
        foreach (var part in itemToEquip.equipment.armorParts)
        {
            if (part.partID > -1)
            {
                characterManager.ActivatePart(part.bodyType, part.partID);
            }
            else
            {
                characterManager.DeactivatePart(part.bodyType);
            }
        }
    }

    private void OnItemAdded(Item itemToPickedUp)
    {
        int i = (int)itemToPickedUp.equipment.armorType;
        equipmentSlots[i] = itemToPickedUp;
        EquipItem(itemToPickedUp);
    }

    private void OnDisable()
    {
        save.SaveToFile("EquipmentList", equipmentSlots);
    }

}

