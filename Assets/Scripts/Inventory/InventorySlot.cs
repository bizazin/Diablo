using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] Button _removeButton;

    private IteM _item;

    public void AddItem(IteM newItem)
    {
        _item = newItem;
       _icon.sprite = _item.Icon;
        _icon.enabled = true;
        _removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
        _removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.Instance.Remove(_item);
    }

    public void UseItem()
    {
        if (_item != null) _item.Use();
    }
}
