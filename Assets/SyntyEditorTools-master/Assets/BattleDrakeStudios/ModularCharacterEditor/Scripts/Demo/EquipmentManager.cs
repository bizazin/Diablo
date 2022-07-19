using BattleDrakeStudios.ModularCharacters;
using Newtonsoft.Json;
using System;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Equipment[] EquipmentSlots { get; set; }

    [SerializeField] private ModularCharacterManager characterManager;
    [SerializeField] private ModularCharacterManager playerView;
    [SerializeField] private RemoteConfigStorage rem;
    [SerializeField] private SaveManager save;
    [SerializeField] private PlayerStats playerStats;

    private void OnEnable()
    {
        EventsManager.OnItemEquipped += EquipItem;
        EventsManager.OnItemUnequipped += Unequip;
        EventsManager.OnItemEquippedUI += EquipPlayerPreview;
    }

    private void Awake()
    {
        EquipmentSlots = new Equipment[Enum.GetNames(typeof(EquipmentType)).Length];
        LoadJson();
    }

    private void Start()
    {
        playerStats.ResetStats();
        foreach (var item in EquipmentSlots)
            if (item != null) 
                EquipItem(item);
    }

    private void LoadJson()
    {
        rem = Resources.Load<RemoteConfigStorage>("Storage");
        if (rem.GetConfig(RemoteConfigs.EnableCustomEquipment).Value == "1")
            EquipmentSlots = JsonConvert.DeserializeObject<Equipment[]>(rem.GetConfig(RemoteConfigs.Equipment).DefaultValue);
        else
            EquipmentSlots = save.LoadJsonArray("Equipment", EquipmentSlots);
    }

    private void EquipItem(Equipment equipment)
    {
        var playerEquipment = characterManager;
        foreach (var part in equipment.ArmorParts)
        {
            if (part.partID > -1)
                playerEquipment.ActivatePart(part.bodyType, part.partID);
            else
                playerEquipment.DeactivatePart(part.bodyType);
        }
        
        int idEquip = (int)equipment.ArmorType;
        EquipmentSlots[idEquip] = equipment;
        
        SetPlayerStats();
        EventsManager.OnStatsChanged.Invoke();
    }

    private void EquipPlayerPreview(PlayerPreview playerPreview)
    {
        playerView = playerPreview.GetComponentInChildren<ModularCharacterManager>();
        foreach (var equipItem in EquipmentSlots)
            if (equipItem != null)
                foreach (var part in equipItem.ArmorParts)
                {
                    if (part.partID > -1)
                        playerView.ActivatePart(part.bodyType, part.partID);
                    else
                        playerView.DeactivatePart(part.bodyType);
                }
    }
    

    private void Unequip(Equipment unequip)
    {
        foreach (var part in unequip.ArmorParts)
        {
            characterManager.ActivatePart(part.bodyType, 0);
            playerView.ActivatePart(part.bodyType, 0);
        }

        int idEquip = (int)unequip.ArmorType;
        EquipmentSlots[idEquip] = null;

        SetPlayerStats();
        EventsManager.OnStatsChanged.Invoke();
    }

    private void SetPlayerStats()
    {
        playerStats.ResetStats();
        foreach (var equipmentSlot in EquipmentSlots)
            if (equipmentSlot!=null)
            {
                playerStats.Damage += equipmentSlot.Stats.Damage;
                playerStats.Defence += equipmentSlot.Stats.Defence;
                playerStats.Speed += equipmentSlot.Stats.Speed;
                playerStats.CriticalChance += equipmentSlot.Stats.CriticalChance;
                playerStats.CriticalDamage += equipmentSlot.Stats.CriticalDamage;
            }
    }
    
    private void OnDisable()
    {
        EventsManager.OnItemEquipped -= EquipItem;
        EventsManager.OnItemUnequipped -= Unequip;
        EventsManager.OnItemEquippedUI -= EquipPlayerPreview;

        save.SaveToFile("Equipment", EquipmentSlots);
    }

}

