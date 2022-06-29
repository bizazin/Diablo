using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interectable : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            Interact();
    }
}
