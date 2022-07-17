using System;
using System.Runtime.Serialization;
using BattleDrakeStudios.ModularCharacters;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
[DataContract]
public class EquipmentSlot : MonoBehaviour
{
    [DataMember][SerializeField] private Image image;
    [DataMember] [SerializeField] private Sprite defaultSprite;
    [DataMember][SerializeField] private EquipmentType equipmentType;
    [DataMember]private Equipment recievedEquipment;
    [DataMember]private int spriteID;

    private Button button;

    public Image Image { get { return image; } set { image = value; } }
    public Sprite DefaultSprite { get { return defaultSprite; } set { defaultSprite = value; } }
    public Equipment RecievedEquipment { get { return recievedEquipment; } set { recievedEquipment = value; } }
    public EquipmentType ArmorType { get { return equipmentType; } set { equipmentType = value; } }
    public bool IsFilled { get; private set; }
    public int SpriteID { get; private set; }

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