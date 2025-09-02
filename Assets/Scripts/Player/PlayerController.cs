using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private IMovable playerMovement;
    private IPlayerAnimator playerAnimator;
    private IGroundDetector groundDetector;

    private InputSystem_Actions inputActions;
    private Vector2 playerInput;

    private Player player;

    private void Awake()
    {
        playerMovement = GetComponent<IMovable>();
        groundDetector = GetComponent<IGroundDetector>();
        playerAnimator = GetComponent<IPlayerAnimator>();

        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Move.canceled += Move;

        player = new Player(3, true);
    }

    private void FixedUpdate()
    {
        playerMovement.Move(playerInput);
        PlayerInputHandler();
    }

    private void Move(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
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
        else
        {
            playerAnimator.PlayIdle();
        }
    }
}
