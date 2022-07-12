using BattleDrakeStudios.ModularCharacters;
using Newtonsoft.Json;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private Equipment[] equipmentSlots;

    [SerializeField] private ModularCharacterManager characterManager;

    [SerializeField] private RemoteConfigStorage rem;

    [SerializeField] private Equipment[] defaultEquipment;

    private SaveManager save;

    private void OnEnable()
    {
        EventsManager.OnItemPickedUp += OnEquipmentAdded;
        EventsManager.OnItemEquipped += EquipItem;
        EventsManager.OnItemUnequipped += Unequip;
    }

    private void Start()
    {

        save = GetComponent<SaveManager>();
        if (rem.GetConfig(RemoteConfigs.EnableCustomInventory).Value == "1")
        {
            equipmentSlots = JsonConvert.DeserializeObject<Equipment[]>(rem.GetConfig(RemoteConfigs.Inventory).DefaultValue);
        }
        else
        {
            equipmentSlots = save.LoadJsonArray("EquipmentList", equipmentSlots);
        }


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
    }

    public void Unequip(Equipment unequip)
    {
        foreach (var part in unequip.ArmorParts)
            characterManager.ActivatePart(part.bodyType, 0);
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
        save.SaveToFile("EquipmentList", equipmentSlots);
    }

}

