using BattleDrakeStudios.ModularCharacters;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    [SerializeField] private List<Item> equipmentSlots;

    [SerializeField] private ModularCharacterManager characterManager;

    private void Start()
    {
       // equipmentSlots = SaveManager.Instance.LoadJsonList("EquipmentList", equipmentSlots);
        EventManager.Instance.OnItemPickedUp.AddListener(OnItemAdded);

        foreach (var item in equipmentSlots)
        {
            EquipItem(item);
        }
    }

    private void EquipItem(Item itemToEquip)
    {

        foreach (var part in itemToEquip.modularArmor.armorParts)
        {
            if (part.partID > -1)
            {
                characterManager.ActivatePart(part.bodyType, part.partID);
                //ColorPropertyLinker[] armorColors = itemToEquip.modularArmor.armorColors;
                /*for (int i = 0; i < armorColors.Length; i++)
                {
                    characterManager.SetPartColor(part.bodyType, part.partID, armorColors[i].property, armorColors[i].color);
                }*/
            }
            else
            {
                characterManager.DeactivatePart(part.bodyType);
            }
        }
    }

    private void OnItemAdded(Item itemToPickedUp)
    {
        equipmentSlots.Add(itemToPickedUp);
        EquipItem(itemToPickedUp);
    }

   /* private void OnDisable()
    {
        SaveManager.Instance.SaveToFile("EquipmentList" , equipmentSlots);
    }*/

}

