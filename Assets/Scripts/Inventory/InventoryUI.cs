using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform slotsParent;
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform playerPreviewContainer;
    [SerializeField] private PlayerPreview playerPreviewPrefab;

    [SerializeField] private Button removeButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button openButton;

    [SerializeField] private ConfirmDeleteWindow confirmDeleteWindow;
    [SerializeField] private EquipmentSlot[] equipmentSlots;
    [SerializeField] private Image newNotif;

    private InventorySlot selectedSlot;

    private void OnEnable()
    {
        EnableEvents();
        EnableButtons();
    }

    private void EnableEvents()
    {
        EventsManager.OnItemClicked += ChangeSelectedSlot;
        EventsManager.OnEquipmentClicked += ChangeEquipmentSlot;

        EventsManager.OnItemAdded += AddItemToUI;
        EventsManager.OnItemDeleted += DeleteItemFromUI;

        EventsManager.OnCheckingForNewItems += ToggleNewNotification;
    }

    private void EnableButtons()
    {
        equipButton.onClick.AddListener(EquipItem);
        removeButton.onClick.AddListener(ConfirmDeleteItem);
        backButton.onClick.AddListener(CloseInventoryWindow);
        openButton.onClick.AddListener(OpenInventoryWindow);
    }

    private void OpenInventoryWindow()
    {
        GetComponentInChildren<InventoryWindow>().Open();
        var playerPreview = Instantiate(playerPreviewPrefab, playerPreviewContainer.transform);
        EventsManager.OnItemEquippedUI?.Invoke(playerPreview);
    }

    private void CloseInventoryWindow()
    {
        GetComponentInChildren<InventoryWindow>().Close();
        selectedSlot?.Deselect();
        Destroy(playerPreviewContainer.GetComponentInChildren<PlayerPreview>().gameObject);
    }

    private void AddItemToUI(Item item)
    {
        CreateFrameForItem().AddItem(item);
    }

    private void DeleteItemFromUI()
    {
        selectedSlot.DeleteSlot();
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

    private InventorySlot CreateFrameForItem()
    {
        return Instantiate(slotPrefab, slotsParent);
    }

    public void EquipItem()
    {
        if (selectedSlot != null && selectedSlot.Item is Equipment currentEquipment)
        {
            EventsManager.OnItemEquipped?.Invoke(currentEquipment);

            var playerPreview = GetComponentInChildren<PlayerPreview>();
            EventsManager.OnItemEquippedUI?.Invoke(playerPreview);

            ChangeCurrentEquipmentImage(currentEquipment);

            Destroy(selectedSlot.gameObject);
        }
    }

    private void Unequip(Equipment equipment)
    {
        EventsManager.OnItemUnequipped?.Invoke(equipment);
        EventsManager.OnUnequippedOrDeletedUI?.Invoke(equipment);

        var playerPreview = GetComponentInChildren<PlayerPreview>();
        EventsManager.OnItemUnequippedUI?.Invoke(playerPreview);
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

        EventsManager.OnStatsUIChanged?.Invoke(selectedSlot);
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

    private void ToggleNewNotification()
    {
        newNotif.enabled = Inventory.Instance.CheckForNewItems();
    }

    private void ConfirmDeleteItem()
    {
        if (selectedSlot != null)
        {
            confirmDeleteWindow.Initialize(selectedSlot);
            confirmDeleteWindow.Open();
        }
    }

    private void OnDisable()
    {
        DisableEvents();
        DisableButtons();
    }

    private void DisableEvents()
    {
        EventsManager.OnItemClicked -= ChangeSelectedSlot;
        EventsManager.OnEquipmentClicked -= ChangeEquipmentSlot;

        EventsManager.OnItemAdded -= AddItemToUI;
        EventsManager.OnItemDeleted -= DeleteItemFromUI;

        EventsManager.OnCheckingForNewItems -= ToggleNewNotification;
    }

    private void DisableButtons()
    {
        equipButton.onClick.RemoveListener(EquipItem);
        removeButton.onClick.RemoveListener(ConfirmDeleteItem);
        backButton.onClick.RemoveListener(CloseInventoryWindow);
        openButton.onClick.RemoveListener(OpenInventoryWindow);
    }
}
