using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private Transform _player;
    private bool _isInteracting;

    private void Update()
    {
        float distance = Vector3.Distance(_player.position, transform.position);
        if (distance <= _radius && !_isInteracting)
        {
            Interact();
            _isInteracting = true;
        }
        
        if (distance > _radius) _isInteracting = false;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
