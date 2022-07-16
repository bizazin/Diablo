using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform slotsParent;
    [SerializeField] private InventorySlot slotPrefab;
    
    [SerializeField] private Button removeButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button openButton;
    
    [SerializeField] private ConfirmDeleteWindow confirmDeleteWindow;
    [SerializeField] private EquipmentSlot[] equipmentSlots;
    
    private InventorySlot selectedSlot;

    private void Start()
    {
        EventsManager.OnItemClicked += ChangeSelectedSlot;
        EventsManager.OnEquipmentClicked += ChangeEquipmentSlot;

        EventsManager.OnItemAdded += AddItemToUI;
        EventsManager.OnItemDeleted += DeleteItemFromUI;

        equipButton.onClick.AddListener(EquipItem);
        removeButton.onClick.AddListener(ConfirmDeleteItem);
        backButton.onClick.AddListener(CloseInventoryWindow);
        openButton.onClick.AddListener(OpenInventoryWindow);
    }

    private void CloseInventoryWindow()
    {
        GetComponentInChildren<InventoryWindow>().Close();
        selectedSlot?.Deselect();
    }

    private void OpenInventoryWindow()
    {
        GetComponentInChildren<InventoryWindow>().Open();
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

 //       ResetSliders();

        EventsManager.OnStatsUIChanged?.Invoke(selectedSlot);
    }

/*    private void ChangeUI()
    {
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();

        if (slots.Length > inventory.Items.Count) selectedSlot.DeleteSlot();

        else CreateFrameForItem().AddItem(inventory.Items.Last());
    }*/

    private void DeleteItemFromUI()
    {
        selectedSlot.DeleteSlot();
    }

    private void AddItemToUI(Item item)
    {
        CreateFrameForItem().AddItem(item);
    }

    private InventorySlot CreateFrameForItem()
    {
        return Instantiate(slotPrefab, slotsParent);
    }

    public void EquipItem()
    {
        if (selectedSlot != null && selectedSlot.Item is Equipment currentEquipment)
        {
            EventsManager.OnItemEquipped?.Invoke(currentEquipment);

            ChangeCurrentEquipmentImage(currentEquipment);

            Destroy(selectedSlot.gameObject);
        }
    }

    private void Unequip(Equipment equipment)
    {
        EventsManager.OnItemUnequipped?.Invoke(equipment);
        EventsManager.OnUnequippedOrDeletedUI?.Invoke(equipment);
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
