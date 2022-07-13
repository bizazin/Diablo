using BattleDrakeStudios.ModularCharacters;
using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType ArmorType;
    public BodyPartLinker[] ArmorParts;
}

