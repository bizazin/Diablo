using BattleDrakeStudios.ModularCharacters;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Equipment recievedEquipment;
    [SerializeField] private EquipmentType equipmentType;

    private Button button;

    public Image Image { get { return image; } set { image = value; } }
    public Sprite DefaultSprite { get { return defaultSprite; } set { defaultSprite = value; } }
    public Equipment RecievedEquipment { get { return recievedEquipment; } set { recievedEquipment = value; } }
    public EquipmentType ArmorType { get { return equipmentType; } set { equipmentType = value; } }
    public bool IsFilled { get; private set; }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnPointerClick);
    }

    private void FillSlot()
    {
        IsFilled = true;
        Image.color = Color.white;
    }

    public void Empty()
    {
        if (this != null)
        {
            RecievedEquipment = null;
            IsFilled = false;
            Image.color = new Color32(95, 78, 100, 255);
            Image.sprite = DefaultSprite;
        }
    }

    public void AddEquipment(Equipment equipment)
    {
        if (equipment != null)
        {
            RecievedEquipment = equipment;
            Image.sprite = equipment.Icon;
            FillSlot();
        }
    }

    public void OnPointerClick()
    {
        EventsManager.OnEquipmentClicked?.Invoke(this);
    }
}