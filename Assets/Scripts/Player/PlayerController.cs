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
        groundDetector = GetComponentInChildren<IGroundDetector>();
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

        playerMovement.CoyoteTimeCounter(groundDetector.IsGround());

        if (!groundDetector.IsGround())
        {
            playerMovement.JumpBufferCounter(inputActions.Player.Jump.triggered);
        }           
    }

    private void Move(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (playerMovement.CanJump() || groundDetector.IsGround())
        {
            playerMovement.Jump();
        }           
    }

    private void PlayerInputHandler()
    {
        if (playerInput.x > 0)
        {
            playerAnimator.Flip(false);   
            if (groundDetector.IsGround())
            {
                playerAnimator.PlayMove();
            }
        }
        else if (playerInput.x < 0)
        {
            playerAnimator.Flip(true);
            if (groundDetector.IsGround())
            {
                playerAnimator.PlayMove();
            }
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
        else if (groundDetector.IsGround())
        {
            playerMovement.ResetGravity();
        }    
    }    

    private void StandHandle()
    {
        if (playerInput.sqrMagnitude < 0.01f && groundDetector.IsGround())
        {
            playerAnimator.PlayIdle();
        }
    }
}
