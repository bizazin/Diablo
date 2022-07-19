using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Character movement stats")]
    [SerializeField] private float rotateSpeed;

    private Player player;
    private float gravityForce;
    private CharacterController characterController;
    private Vector3 velocityDirection;

    private void Start()
    {
        gravityForce = 9.8f;
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        EventsManager.OnDeath += BlockMovement;
    }

    private void LateUpdate()
    {
        GravityHandling();
    }

    public void MoveCharacter(Vector3 moveDirection)
    {
        int speed = Mathf.Clamp(4*(player.PlayerStats.Speed/100),4,8);
        velocityDirection.x = moveDirection.x * speed;
        velocityDirection.z = moveDirection.z * speed;
        characterController.Move(velocityDirection * Time.deltaTime);
    }

    public void RotateCharacter(Vector3 moveDirection)
    {
        if (Vector3.Angle(transform.forward, moveDirection) > 0)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, rotateSpeed, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void GravityHandling()
    {
        if (!characterController.isGrounded)
            velocityDirection.y -= gravityForce * Time.deltaTime;
        else
            velocityDirection.y = -0.5f;
    }

    public void BlockMovement()
    {
        player.PlayerStats.Speed = 0;
        rotateSpeed = 0;
    }
    
}
