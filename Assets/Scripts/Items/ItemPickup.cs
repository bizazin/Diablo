using BattleDrakeStudios.ModularCharacters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item _item;
    /*public override void Interact()
    {
        base.Interact();
        PickUp();
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Picking up " + _item.itemName);
        // bool wasPickedUp = Inventory.Instance.Add(_item);
        EventManager.Instance.OnItemPickedUp?.Invoke(_item);
        Destroy(gameObject);
    }
}
 