using BattleDrakeStudios.ModularCharacters;
using bizazin;
using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static Action<Equipment> OnItemPickedUp;
    public static Action<InventorySlot> OnInventorySlotFocused;
    public static Action<Equipment> OnItemEquipped;
}
