using Unity.Jobs;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private IPlayerMovement playerMovement;
    private IPlayerAnimator playerAnimator;
    private IGroundDetector groundDetector;

    private InputSystem_Actions inputActions;
    private Vector2 playerInput;

    private Player player;

    private void Awake()
    {
        playerMovement = GetComponent<IPlayerMovement>();
        groundDetector = GetComponent<IGroundDetector>();
        playerAnimator = GetComponent<IPlayerAnimator>();

        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Move.canceled += Move;
        inputActions.Player.Jump.performed += Jump;

        player = new Player(3, true);
    }

    private void FixedUpdate()
    {
        playerMovement.Move(playerInput);      
    }

    private void Update()
    {
        PlayerInputHandler();
        JumpHandle();
        FallHandle();
        StandHandle();
    }

    private void Move(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        playerMovement.Jump();
    }

    private void PlayerInputHandler()
    {
        if (playerInput.x > 0)
        {
            playerAnimator.Flip(false);
            playerAnimator.PlayMove();
        }
        else if (playerInput.x < 0)
        {
            playerAnimator.Flip(true);
            playerAnimator.PlayMove();
        }
    }

    private void JumpHandle()
    {
        if (playerMovement.GetVelocity().y > 0.1f)
        {
            playerAnimator.PlayJump();
        }
    }    

    private void FallHandle()
    {
        if (playerMovement.GetVelocity().y < -0.1f)
        {
            playerAnimator.PlayFall();
            playerMovement.FallAccelaration();
        }    
        else if (playerMovement.GetVelocity().y == 0)
        {
            playerMovement.ResetGravity();
        }    
    }    

    private void StandHandle()
    {
        if (playerMovement.GetVelocity() == Vector2.zero)
        {
            playerAnimator.PlayIdle();
        }
    }
}
