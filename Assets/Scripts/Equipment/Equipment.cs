using BattleDrakeStudios.ModularCharacters;
using UnityEngine;

namespace bizazin
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Equipment")]
    public class Equipment : Item
    {
        public EquipmentType ArmorType;
        public BodyPartLinker[] ArmorParts;
        public bool IsBodyPart;
    }
}
