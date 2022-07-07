using bizazin;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform �ontents;
    [SerializeField] private InventorySlot slot;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private InventorySlot selectedSlot;
    [SerializeField] private ConfirmSellWindow confirmDeleteWindow;
    [SerializeField] private EquipmentSlot[] equipmentSlots;

    private Inventory inventory;
    private InventorySlot[] inventorySlots;
    

    private void Start()
    {
        inventory = Inventory.Instance;
        inventory.OnItemChangedCallback += UpdateUI;

        equipButton.onClick.AddListener(EquipItem);
        EventsManager.OnItemClicked += ChangeSelectedSlot;
        EventsManager.OnEquipmentClicked += ChangeEquipmentSlot;
        removeButton.onClick.AddListener(ConfirmDeleteItem);
    }

    private void ChangeEquipmentSlot(EquipmentSlot currentSlot)
    {
        if (currentSlot.IsFilled)
        {
            Unequip(currentSlot.RecievedEquipment);
            ReturnEquipmentToSlots(currentSlot.RecievedEquipment);
            currentSlot.Empty();
        }
    }

    private void ReturnEquipmentToSlots(Equipment equipment)
    {
        if (equipment != null)
        {
            InventorySlot newSlot = CreateFrameForItem();
            newSlot.AddItem(equipment);
        }
    }

    private void ChangeSelectedSlot(InventorySlot currentSlot)
    {
        InventorySlot previousSlot = null;
        if (!currentSlot.IsSelected)
        {
            previousSlot = selectedSlot?.Deselect();
            selectedSlot = currentSlot?.Select();
        }
        else
            selectedSlot = currentSlot?.Deselect();
    }

    private void UpdateUI()
    {
        CreateFrameForItem();
        inventorySlots = �ontents.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.Items.Count)
                inventorySlots[i].AddItem(inventory.Items[i]);
            else
                inventorySlots[i].DeleteSlot();
        }
    }

    private InventorySlot CreateFrameForItem()
    {
        InventorySlot inventorySlot = Instantiate(slot, �ontents);
        return inventorySlot;
    }

    public void EquipItem()
    {
        if (selectedSlot?.Item is Equipment currentEquipment)
        {
            EventsManager.OnItemEquipped?.Invoke(currentEquipment);
            ChangeCurrentEquipmentImage(currentEquipment);
            Destroy(selectedSlot.gameObject);
        }
    }

    private void Unequip(Equipment equipment)
    {
        EventsManager.OnItemUnequipped?.Invoke(equipment);
    }

    private void ChangeCurrentEquipmentImage(Equipment equipment)
    {
        foreach (var slot in equipmentSlots)
            if (slot.ArmorType == equipment.ArmorType)
            {
                ReturnEquipmentToSlots(slot.RecievedEquipment);
                slot.AddEquipment(equipment);
            }
    }


    private void ConfirmDeleteItem()
    {
        if (selectedSlot != null)
        {
            confirmDeleteWindow.Initialize(selectedSlot);
            confirmDeleteWindow.Open();
        }
    }
}
