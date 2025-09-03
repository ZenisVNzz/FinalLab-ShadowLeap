using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private IPlayerMovement playerMovement;
    private IPlayerAnimator playerAnimator;
    private IGroundDetector groundDetector;
    private IPlayerAttack playerAttack;

    private InputSystem_Actions inputActions;
    private Vector2 playerInput;

    private Player player;

    private SquashAndStretch squashAndStretch;
    private CinemachineImpulseSource impulseSource;
    private bool wasGrounded;

    private void Awake()
    {
        playerMovement = GetComponent<IPlayerMovement>();
        groundDetector = GetComponentInChildren<IGroundDetector>();
        playerAnimator = GetComponent<IPlayerAnimator>();
        playerAttack = GetComponent<IPlayerAttack>();

        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Move.canceled += Move;
        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.Dash.performed += Dash;
        inputActions.Player.Attack.performed += Attack;

        player = new Player(3, true);

        squashAndStretch = GetComponentInChildren<SquashAndStretch>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
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
            impulseSource.GenerateImpulse();
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        playerAttack.Attack(mousePos);
        playerAnimator.PlayAttack();
    }

    private void PlayerInputHandler()
    {
        if (playerInput.x > 0)
        {
            playerAnimator.Flip(false);   
            if (groundDetector.IsGround() && !playerAttack.IsAttacking())
            {
                playerAnimator.PlayMove();
            }
        }
        else if (playerInput.x < 0)
        {
            playerAnimator.Flip(true);
            if (groundDetector.IsGround() && !playerAttack.IsAttacking())
            {
                playerAnimator.PlayMove();
            }
        }
    }

    private void JumpHandle()
    {
        if (playerMovement.GetVelocity().y > 0.1f && !playerAttack.IsAttacking())
        {
            playerAnimator.PlayJump();
        }
    }       

    private void FallHandle()
    {
        if (playerMovement.GetVelocity().y < -0.1f && !playerAttack.IsAttacking())
        {
            playerAnimator.PlayFall();
            playerMovement.FallAccelaration();

            playerMovement.JumpBufferCounter(inputActions.Player.Jump.triggered);
            wasGrounded = false;
        }    
        else if (groundDetector.IsGround())
        {
            playerMovement.ResetGravity();
            playerMovement.ResetDash();

            if (!wasGrounded)
            {
                squashAndStretch.OnLand();
                Transform ground = transform.GetChild(1).transform;
                VFXManager.Instance.Initialize(100001, new Vector2(ground.position.x, ground.position.y + 0.2f));
                wasGrounded = true;
            }                 
        }
    }    

    private void StandHandle()
    {
        if (playerInput.sqrMagnitude < 0.01f && groundDetector.IsGround() && !playerAttack.IsAttacking())
        {
            playerAnimator.PlayIdle();
        }
    }
}
