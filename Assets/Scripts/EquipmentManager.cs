using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private Equipment[] _currentEquipment;

    private void Start()
    {
        int numberSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numberSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipSlot;
        _currentEquipment[slotIndex] = newItem;
    }
}
