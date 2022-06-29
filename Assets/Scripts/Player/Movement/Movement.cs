using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Character movement stats")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    [Header("Gravity handling")]
    private float _gravityForce = 9.8f;
    public float GravityForce 
    { 
        set 
        {
            if (value >= 0)
                _gravityForce = value;
        } 
    }

    [Header("Character components")]
    private CharacterController _characterController;

    [HideInInspector] public Vector3 velocityDirection;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        EventsManager.OnDeath += BlockMovement;
    }

    private void LateUpdate()
    {
        GravityHandling();
    }

    public void MoveCharacter(Vector3 moveDirection)
    {
        velocityDirection.x = moveDirection.x * _moveSpeed;
        velocityDirection.z = moveDirection.z * _moveSpeed;
        _characterController.Move(velocityDirection * Time.deltaTime);
    }

    public void RotateCharacter(Vector3 moveDirection)
    {
        if (Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void GravityHandling()
    {
        if (!_characterController.isGrounded)
            velocityDirection.y -= _gravityForce * Time.deltaTime;
        else
            velocityDirection.y = -0.5f;
    }

    public void BlockMovement()
    {
        _moveSpeed = 0;
        _rotateSpeed = 0;
    }
    
}
