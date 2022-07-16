using BattleDrakeStudios.ModularCharacters;
using Newtonsoft.Json;
using System;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Equipment[] EquipmentSlots { get; set; }

    [SerializeField] private ModularCharacterManager characterManager;
//    private SaveManager save;

    private void OnEnable()
    {
        EventsManager.OnItemPickedUp += OnEquipmentAdded;
        EventsManager.OnItemEquipped += EquipItem;
        EventsManager.OnItemUnequipped += Unequip;
    }

    private void Awake()
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
        EquipmentSlots = new Equipment[Enum.GetNames(typeof(EquipmentType)).Length];


        foreach (var item in EquipmentSlots)
            if (item != null) 
                EquipItem(item);
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
        EquipmentSlots[idEquip] = equipment;
        
        EventsManager.OnStatsChanged.Invoke();
    }

    public void Unequip(Equipment unequip)
    {
        foreach (var part in unequip.ArmorParts)
            characterManager.ActivatePart(part.bodyType, 0);

        int idEquip = (int)unequip.ArmorType;
        EquipmentSlots[idEquip] = null;

        EventsManager.OnStatsChanged.Invoke();
    }

    private void OnEquipmentAdded(Equipment itemToPickedUp)
    {
        int i = (int)itemToPickedUp.ArmorType;
        EquipmentSlots[i] = itemToPickedUp;
        EquipItem(itemToPickedUp);
    }

    private void OnDisable()
    {
        EventsManager.OnItemPickedUp -= OnEquipmentAdded;
        EventsManager.OnItemEquipped -= EquipItem;
       // save.SaveToFile("EquipmentList", equipmentSlots);
    }

}

