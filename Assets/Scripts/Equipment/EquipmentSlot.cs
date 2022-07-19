using System;
using System.Runtime.Serialization;
using BattleDrakeStudios.ModularCharacters;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Image equippedIcon;
    [SerializeField] private Image defaultSprite;
    
    [SerializeField] private EquipmentType equipmentType;
    private Equipment recievedEquipment;
    private int spriteID;

    private Button button;

    public Image EquippedIcon { get { return equippedIcon; } set { equippedIcon = value; } }
    public Image DefaultSprite { get { return defaultSprite; } set { defaultSprite = value; } }
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
        EquippedIcon.color = Color.white;
    }

    public void Empty()
    {
        if (this != null)
        {
            RecievedEquipment = null;
            IsFilled = false;
            EquippedIcon.color = new Color32(95, 78, 100, 255);
            EquippedIcon.enabled = false;
            DefaultSprite.enabled = true;
        }
    }

    public void AddEquipment(Equipment equipment)
    {
        if (equipment != null)
        {
            RecievedEquipment = equipment;
            EquippedIcon.enabled = true;
            DefaultSprite.enabled = false;
            EquippedIcon.sprite = equipment.Icon;
            FillSlot();
        }
    }

    public void OnPointerClick()
    {
        EventsManager.OnEquipmentClicked?.Invoke(this);
    }
}