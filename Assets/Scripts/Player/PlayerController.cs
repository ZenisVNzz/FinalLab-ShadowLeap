using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IAttackable
{
    private IPlayerMovement playerMovement;
    private IPlayerAnimator playerAnimator;
    private IGroundDetector groundDetector;
    private IPlayerAttack playerAttack;

    private InputSystem_Actions inputActions;
    private Vector2 playerInput;

    private Player player;

    private SquashAndStretch squashAndStretch;   
    private bool wasGrounded;

    //UI
    [SerializeField] private HealthUI healthUI;

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

        EventManager.instance.Register("OnPlayerDead", OnPlayerDeadHandler);    

        squashAndStretch = GetComponentInChildren<SquashAndStretch>();
    }

    private void OnEnable()
    {
        player = new Player(3, true);
        inputActions.Player.Enable();
        healthUI.Initialize(player.lives);
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

    public void TakeDamage(int damage)
    {
        player.lives -= damage;
        healthUI.UpdateHealth(player.lives);
        if (player.lives <= 0)
        {
            EventManager.instance.Trigger("OnPlayerDead");
            Debug.Log("Player Dead");
        }
        else
        {
            playerAnimator.Play(PlayerAnimationState.Player_Hurt);
            Debug.Log("Player took damage. Remaining lives: " + player.lives);
        }
    }

    private void OnPlayerDeadHandler()
    {
        playerAnimator.Play(PlayerAnimationState.Player_Death);
        inputActions.Player.Disable();
        this.enabled = false;
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
            CameraShake.instance.ShakeCamera();
        }
    }

    private void Attack(InputAction.CallbackContext context)
    {      
        playerAttack.Attack();
        playerAnimator.Play(PlayerAnimationState.Player_Attack);
    }

    private void PlayerInputHandler()
    {
        if (playerInput.x > 0)
        {
            playerAnimator.Flip(false);   
            if (groundDetector.IsGround() && !playerAttack.IsAttacking())
            {
                playerAnimator.Play(PlayerAnimationState.Player_Move);
                SFXManager.instance.PlaySFX("200001");
            }
        }
        else if (playerInput.x < 0)
        {
            playerAnimator.Flip(true);
            if (groundDetector.IsGround() && !playerAttack.IsAttacking())
            {
                playerAnimator.Play(PlayerAnimationState.Player_Move);
                SFXManager.instance.PlaySFX("200001");
            }
        }
        else
        {
            SFXManager.instance.StopSFX("200001");
            if (!groundDetector.IsGround())
            {
                SFXManager.instance.StopSFX("200001");
            }
        }
    }

    private void JumpHandle()
    {
        if (playerMovement.GetVelocity().y > 0.1f && !playerAttack.IsAttacking())
        {
            playerAnimator.Play(PlayerAnimationState.Player_Jump);
        }
    }       

    private void FallHandle()
    {
        if (playerMovement.GetVelocity().y < -0.1f && !playerAttack.IsAttacking())
        {
            playerAnimator.Play(PlayerAnimationState.Player_Fall);
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
            playerAnimator.Play(PlayerAnimationState.Player_Idle);
        }
    }
}
