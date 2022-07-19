using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image newNotificationSlot;
    [SerializeField] private Image itemRarity;
    [SerializeField] private Sprite[] itemRarityBorders;
    [SerializeField] private GameObject focus;
    [SerializeField] private TMP_Text quantityText;
    
    private Button button;

    public Item Item;
    public bool IsSelected { get; private set; }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPointerClick);
        itemRarity.sprite = itemRarityBorders[(int) Item.Stats.Rar];
        newNotificationSlot.enabled = Item.IsNew;
    }

    public InventorySlot Select()
    {
        focus.SetActive(true);
        IsSelected = true;
        Item.IsNew = false;
        newNotificationSlot.enabled = false;
        EventsManager.OnCheckingForNewItems.Invoke();
        return this;
    }

    public InventorySlot Deselect()
    {
        if (this != null)
        {
            focus.SetActive(false);
            IsSelected = false;
        }
        return null;
    }

    public void OnPointerClick()
    {
        EventsManager.OnItemClicked?.Invoke(this);
    }

    public void AddItem(Item newItem)
    {
        Item = newItem;
        icon.sprite = Item.Icon;

        icon.enabled = true;
    }

    public void DeleteSlot()
    {
        Destroy(gameObject);
    }
}
