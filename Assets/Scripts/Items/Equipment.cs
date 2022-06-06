using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{ 
    [SerializeField] private EquipmentSlot _equipSlot;
    [SerializeField] private SkinnedMeshRenderer _mesh;
    [SerializeField] private int _armorModifier;
    [SerializeField] private int _damageModifier;
    
    public EquipmentSlot EquipSlot { get { return _equipSlot; } set { _equipSlot = value; } }
    public SkinnedMeshRenderer Mesh { get { return _mesh; } set { _mesh = value; } }
    public int ArmorModifier { get { return _armorModifier; } set { _armorModifier = value; } }
    public int DamageModifier { get { return _damageModifier; } set { _damageModifier = value; } }

    public override void Use()
    {
        base.Use();
        EquipmentManager.Instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }