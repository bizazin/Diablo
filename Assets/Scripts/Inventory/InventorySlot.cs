using bizazin;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _focus;
    
    private Button _button;

    public Item Item;
    public bool IsFocused { get; private set; }


    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeFocusedItem);
        _button.onClick.AddListener(ChangeFocusedItemInventory);
    }

    public void ChangeFocusedItem()
    {
        IsFocused = _focus.activeSelf;
        _focus.SetActive(!IsFocused);
        IsFocused = !IsFocused;
    }

    public void ChangeFocusedItemInventory()
    {
        EventsManager.OnInventorySlotFocused.Invoke(this);
    }

    public void AddItem(Item newItem)
    {
        Item = newItem;
        _icon.sprite = Item.Icon;
        _icon.enabled = true;
    }

    public void ClearSlot()
    {
        Item = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }

    public void DeleteSlotFromUI()
    {
        Destroy(gameObject);
    }

    public void UseItem()
    {
//        if (_item != null) _item.Use();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ChangeFocusedItem);
        _button.onClick.RemoveListener(ChangeFocusedItemInventory);
    }
}
