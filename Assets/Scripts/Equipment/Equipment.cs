using BattleDrakeStudios.ModularCharacters;
using Newtonsoft.Json;
using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Equipment")]
[JsonObject(MemberSerialization.OptIn)]
public class Equipment : Item
{
    [JsonProperty] public EquipmentType ArmorType;
    [JsonProperty] public BodyPartLinker[] ArmorParts;
}

