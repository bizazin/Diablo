using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Movement))]
public class JumpHandler : MonoBehaviour
{
    [Header("Jump stats")]
    [SerializeField] private float _maxJumpTime;
    [SerializeField] private float _maxJumpHeight;
    private float _startJumpVelocity;

    [Header("Character components")]
    private Movement _characterPlayer;
    private CharacterController _characterController;

    private void Start()
    {
        _characterPlayer = GetComponent<Movement>();
        _characterController = GetComponent<CharacterController>();
        float maxHeightTime = _maxJumpTime / 2;
        _characterPlayer.GravityForce = (2 * _maxJumpHeight) / (maxHeightTime * maxHeightTime);
        _startJumpVelocity = (2 * _maxJumpHeight) / maxHeightTime;
    }
    
    public void HandleJump()
    {
        if (_characterController.isGrounded)
            _characterPlayer.velocityDirection.y = _startJumpVelocity;
    }
}
