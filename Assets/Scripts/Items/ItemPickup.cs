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
        Debug.Log("Picking up " + _item.ItemName);
        
        Inventory.Instance.Add(_item);
        Destroy(gameObject);
    }
}
 