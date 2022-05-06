using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickForMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Movement _movement;
    [SerializeField] private float _moveSpeed;

    [Header("Animator components")]
    [SerializeField] private Animator _animator;

    private void Update()
    {
        var sideForce = _joystick.Horizontal * _moveSpeed;
        var forwardForce = _joystick.Vertical * _moveSpeed;
        _movement.MoveCharacter(new Vector3(sideForce, 0, forwardForce));

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Run();
            _movement.RotateCharacter(new Vector3(sideForce, 0, forwardForce));
//            transform.rotation = Quaternion.LookRotation(new Vector3(sideForce, 0, forwardForce));
        }
        else StopRun();
    }

    private void Run()
    {
        _animator.SetBool("isRunning", true);
    }

    private void StopRun()
    {
        _animator.SetBool("isRunning", false);
    }
}
