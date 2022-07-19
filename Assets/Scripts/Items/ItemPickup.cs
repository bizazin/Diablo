using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ItemPickup : Interectable
{
    [SerializeField] private ItemStats stats;
    private bool canPickUp;

    public Item Item;
    
    private void Start()
    {
        StartCoroutine(PickUpDelay());
        ShootItem();

        if (Item == null)
        {
            Item = DropManager.Instance.SetupItem();
            stats = Item.Stats;
        }
        
        GetComponentInChildren<OrbColor>().SetColor();
    }

    private void ShootItem()
    {
        var pos = new Vector3(Random.Range(-.5f, .5f), 2, Random.Range(-.5f, .5f));
        GetComponent<Rigidbody>().AddForce(pos, ForceMode.Impulse);
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
        Debug.Log("Picking up " + Item.Name);
        bool wasPickedUp = Inventory.Instance.Add(Item);
        EventsManager.OnCheckingForNewItems?.Invoke();

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
