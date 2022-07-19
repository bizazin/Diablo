using UnityEngine;

public class TestEnemy : Interectable
{
    [SerializeField] private ItemPickup itemToDrop;

    public override void Interact()
    {
        base.Interact();
        DropItem();
    }

    private void DropItem()
    {
        var pos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Instantiate(itemToDrop, pos, Quaternion.identity);

        Destroy(gameObject);
    }
}
