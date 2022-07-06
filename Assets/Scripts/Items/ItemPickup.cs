using bizazin;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ItemPickup : Interectable
{
    [SerializeField] private ItemStats stats;
    private Rigidbody rb;

    public Item item;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        var pos = transform.position;
        rb.AddForce(new Vector3(Random.Range(-.5f,.5f), 2, Random.Range(-.5f,.5f)), ForceMode.Impulse);
        item = DropManager.Instance.SetupItem();
        stats = item.Stats;
    }

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Picking up " + item.ItemName);
        bool wasPickedUp = Inventory.Instance.Add(item);
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
