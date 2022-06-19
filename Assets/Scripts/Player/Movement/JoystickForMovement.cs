using UnityEngine;

public class JoystickForMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Movement movement;
    [SerializeField] private float moveSpeed;

    [Header("Animator components")]
    [SerializeField] private PlayerAnimationController animator;

    private void Start()
    {
        animator.Idle();
    }

    private void Update()
    {
        var sideForce = -(joystick.Horizontal) * moveSpeed;
        var forwardForce = -(joystick.Vertical) * moveSpeed;
        movement.MoveCharacter(new Vector3(sideForce, 0, forwardForce));

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            animator.Run();
            movement.RotateCharacter(new Vector3(sideForce, 0, forwardForce));
        }
        else animator.Idle();
    }
}
