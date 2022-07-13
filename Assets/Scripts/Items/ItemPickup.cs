using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ItemPickup : Interectable
{
    [SerializeField] private ItemStats stats;
    private Rigidbody rb;
    public Item item;
    private bool canPickUp;
    
    private void Start()
    {
        StartCoroutine(PickUpDelay());
        rb = GetComponent<Rigidbody>();
        var pos = transform.position;
        rb.AddForce(new Vector3(Random.Range(-.5f,.5f), 2, Random.Range(-.5f,.5f)), ForceMode.Impulse);
        if (item == null)
        {
            item = DropManager.Instance.SetupItem();
            stats = item.Stats;
        }
    }

    public override void Interact()
    {
        if (canPickUp)
        {
            base.Interact();
            PickUp();
        }
    }

    private IEnumerator PickUpDelay()
    {
        yield return new WaitForSeconds(1f);
        canPickUp = true;
    }

    private void PickUp()
    {
        Debug.Log("Picking up " + item.Name);
        bool wasPickedUp = Inventory.Instance.Add(item);
        if (wasPickedUp)
            Destroy(gameObject);
    }
}
