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

    private SquashAndStretch squashAndStretch;
    private bool wasGrounded;

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
        inputActions.Player.Dash.performed += Dash;

        player = new Player(3, true);

        squashAndStretch = GetComponentInChildren<SquashAndStretch>();
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

        playerMovement.DashHandle();
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
            squashAndStretch.OnJump();
        }           
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if (playerMovement.CanDash())
        {
            playerMovement.Dash(playerInput);
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

            playerMovement.JumpBufferCounter(inputActions.Player.Jump.triggered);
        }    
        else if (groundDetector.IsGround())
        {
            playerMovement.ResetGravity();
            playerMovement.ResetDash();

            if (!wasGrounded)
            {
                squashAndStretch.OnLand();
            }                 
        }

        wasGrounded = groundDetector.IsGround();
    }    

    private void StandHandle()
    {
        if (playerInput.sqrMagnitude < 0.01f && groundDetector.IsGround())
        {
            playerAnimator.PlayIdle();
        }
    }
}
