using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private Item _item; 
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Picking up " + _item.Name);
        bool wasPickedUp = Inventory.Instance.Add(_item);
        if (wasPickedUp) Destroy(gameObject);
    }
}
 