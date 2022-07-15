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
    private SaveManager save;

    private void OnEnable()
    {
        EventsManager.OnItemPickedUp += OnEquipmentAdded;
        EventsManager.OnItemEquipped += EquipItem;
        EventsManager.OnItemUnequipped += Unequip;
    }

    private void Awake()
    {
        EquipmentSlots = new Equipment[Enum.GetNames(typeof(EquipmentType)).Length];
        LoadJson();
    }

    private void Start()
    {
        foreach (var item in EquipmentSlots)
            if (item != null) 
                EquipItem(item);
    }

    private void LoadJson()
    {
        rem = Resources.Load<RemoteConfigStorage>("Storage");
        save = GetComponent<SaveManager>();
        if (rem.GetConfig(RemoteConfigs.EnableCustomEquipment).Value == "1")
        {
            EquipmentSlots = JsonConvert.DeserializeObject<Equipment[]>(rem.GetConfig(RemoteConfigs.Equipment).DefaultValue);
        }
        else
        {
            EquipmentSlots = save.LoadJsonArray("Equipment", EquipmentSlots);
        }
    }

    public void EquipItem(Equipment equipment)
    {
        foreach (var part in equipment.ArmorParts)
        {
            if (part.partID > -1)
            {
                characterManager.ActivatePart(part.bodyType, part.partID);
                playerView.ActivatePart(part.bodyType,part.partID);
            }
            else
            {
                characterManager.DeactivatePart(part.bodyType); 
                playerView.DeactivatePart(part.bodyType);
            }
                
        }
        
        int idEquip = (int)equipment.ArmorType;
        EquipmentSlots[idEquip] = equipment;
        
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
        EquipmentSlots[i] = itemToPickedUp;
        EquipItem(itemToPickedUp);
    }

    private void OnDisable()
    {
        EventsManager.OnItemPickedUp -= OnEquipmentAdded;
        EventsManager.OnItemEquipped -= EquipItem;
        save.SaveToFile("Equipment", EquipmentSlots);
    }

}

