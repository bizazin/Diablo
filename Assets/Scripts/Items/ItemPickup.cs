using BattleDrakeStudios.ModularCharacters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item _item;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Picking up " + _item.itemName);
        EventsManager.OnItemPickedUp?.Invoke(_item);
        Destroy(gameObject);
    }
}
 