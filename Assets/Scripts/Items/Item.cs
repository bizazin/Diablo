using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class IteM : ScriptableObject
{
    [SerializeField] private int id = -1;
    [SerializeField] private string _name = "New Item";
    [SerializeField] private Sprite _icon;

    public int ID { get { return id; } set { id = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public Sprite Icon { get { return _icon; } set { _icon = value; } }

    public virtual void Use()
    {
        Debug.Log("Using " + Name);
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}
