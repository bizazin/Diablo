using UnityEngine;

public class TestEnemy : Interectable
{
    public ItemPickup itemToDrop;
 
    public override void Interact()
    {
        base.Interact();
        DropItem();
    }
    
    private void DropItem()
    {
        var droppedItem = Instantiate(itemToDrop, new Vector3(transform.position.x,transform.position.y+1f, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
