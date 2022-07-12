using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmSellWindow : MonoBehaviour, IWindow
{
    [SerializeField] private TMP_Text textInfo;
    [SerializeField] private Button okayButton;
    [SerializeField] private Button cancelButton;

    private InventorySlot slot;

    private void Start()
    {
        okayButton.onClick.AddListener(DeleteItem);
        cancelButton.onClick.AddListener(Close);
    }

    public void Initialize(InventorySlot focusedSlot)
    {
        slot = focusedSlot;
        textInfo.text = $"Do you really want to delete {slot.Item.Name}?";
    }

    private void DeleteItem()
    {
        Inventory.Instance.RemoveItem(slot.Item);
        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
