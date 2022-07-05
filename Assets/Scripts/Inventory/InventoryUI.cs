using bizazin;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform ñontents;
    [SerializeField] private InventorySlot slot;
    [SerializeField] private Button removeButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private InventorySlot selectedSlot;
    [SerializeField] private ConfirmSellWindow confirmDeleteWindow;

    private Inventory inventory;
    private InventorySlot[] slots;

    private void Start()
    {
        inventory = Inventory.Instance;
        inventory.OnItemChangedCallback += UpdateUI;

        equipButton.onClick.AddListener(EquipItem);
        EventsManager.OnItemClicked += ChangeSelectedSlot;
        removeButton.onClick.AddListener(ConfirmDeleteItem);
    }

    private void UpdateUI()
    {
        CreateFrameForItem();
        slots = ñontents.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Items.Count)
                slots[i].AddItem(inventory.Items[i]);
            else
                slots[i].DeleteSlot();
        }
    }

    public void EquipItem()
    {
        if (selectedSlot.Item is Equipment currentEquipment)
            EventsManager.OnItemEquipped?.Invoke(currentEquipment);
    }

    private void CreateFrameForItem()
    {
        Instantiate(slot, ñontents);
    }

    private void ConfirmDeleteItem()
    {
        if (selectedSlot != null)
        {
            confirmDeleteWindow.Initialize(selectedSlot);
            confirmDeleteWindow.Open();
        }
    }

    public void ChangeSelectedSlot(InventorySlot currentSlot)
    {
        InventorySlot previousSlot = null;
        if (currentSlot.IsSelected)
        {
            previousSlot = selectedSlot?.Deselect();
            selectedSlot = currentSlot?.Select();
        }
        else
            selectedSlot = currentSlot?.Deselect();
    }
}
