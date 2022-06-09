using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickForMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Movement _movement;
    [SerializeField] private float _moveSpeed;

    [Header("Animator components")]
    [SerializeField] private PlayerAnimationController animator;

    private void Start()
    {
        animator.Idle();
    }

    private void Update()
    {
        var sideForce = _joystick.Horizontal * _moveSpeed;
        var forwardForce = _joystick.Vertical * _moveSpeed;
        _movement.MoveCharacter(new Vector3(sideForce, 0, forwardForce));

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            animator.Run();
            _movement.RotateCharacter(new Vector3(sideForce, 0, forwardForce));
        }
        else animator.Idle();
    }
}
