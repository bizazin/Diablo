using bizazin;
using System;
using UnityEngine;

public class ItemPickup : Interectable
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
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
