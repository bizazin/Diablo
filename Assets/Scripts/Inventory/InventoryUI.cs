using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private GameObject _inventoryUI;
    private Inventory _inventory;
    private InventorySlot[] _slots;

    private void Start()
    {
        _inventory = Inventory.Instance;
        _inventory.OnItemChangedCallback += UpdateUI;

        _slots = _itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
            _inventoryUI.SetActive(!_inventoryUI.activeSelf);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < _inventory.Items.Count)
                _slots[i].AddItem(_inventory.Items[i]);
            else
                _slots[i].ClearSlot();
        }
    }
}
