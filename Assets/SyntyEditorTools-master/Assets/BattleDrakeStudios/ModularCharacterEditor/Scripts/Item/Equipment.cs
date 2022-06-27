using BattleDrakeStudios.ModularCharacters;
using UnityEngine;
using static BattleDrakeStudios.ModularCharacters.ModularCharacterStatics;

namespace bizazin {

    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Equipment")]
    public class Equipment : Item {
        public ModularArmorType armorType;
      //  public ColorPropertyLinker[] armorColors = { new ColorPropertyLinker(COLOR_PRIMARY), new ColorPropertyLinker(COLOR_SECONDARY), new ColorPropertyLinker(COLOR_LEATHER_PRIMARY),
 //   new ColorPropertyLinker(COLOR_LEATHER_SECONDARY), new ColorPropertyLinker(COLOR_METAL_PRIMARY), new ColorPropertyLinker(COLOR_METAL_SECONDARY), new ColorPropertyLinker(COLOR_METAL_DARK)};
        public BodyPartLinker[] armorParts;
    }
}
