using System.Collections;
using System.Collections.Generic;
using bizazin;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private InventorySlot _slot;
    [SerializeField] private GameObject _contents;
    [SerializeField] private Button _removeButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private InventorySlot _focusedSlot;

    private Inventory _inventory;
    private InventorySlot[] _slots;

    private void Start()
    {
        _inventory = Inventory.Instance;
        _inventory.OnItemChangedCallback += UpdateUI;
        _removeButton.onClick.AddListener(RemoveFocusedSlot);
        _equipButton.onClick.AddListener(EquipItem);
        EventsManager.OnInventorySlotFocused += ChangeFocusedSlot;
       
    }

    private void UpdateUI()
    {
        CreateFrameForItem();
        _slots = _itemsParent.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < _inventory.Items.Count)
                _slots[i].AddItem(_inventory.Items[i]);
            else
                _slots[i].ClearSlot();
        }
    }

    public void EquipItem()
    {
        if (_focusedSlot.Item is Equipment currentEquipment)
            EventsManager.OnItemEquipped?.Invoke(currentEquipment);
    }

    private void CreateFrameForItem()
    {
        Instantiate(_slot, _contents.transform);
    }

    private void RemoveFocusedSlot()
    {
        if (_focusedSlot != null)
        {
            _focusedSlot.DeleteSlotFromUI();

            Inventory.Instance.Items.Remove(_focusedSlot.Item);
        }
    }

    public void ChangeFocusedSlot(InventorySlot inventorySlot)
    {
        if (_focusedSlot!=null)
        {
            _focusedSlot.ChangeFocusedItem();
            _focusedSlot = inventorySlot;
        }
        else
            _focusedSlot = inventorySlot;
    }

    private void FocusedSlot()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFocused)
                _focusedSlot = slot;
        }
       // return _focusedSlot;
    }
}
