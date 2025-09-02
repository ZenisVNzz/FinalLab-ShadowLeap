using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private IMovable playerMovement;
    private IGroundDetector groundDetector;

    private InputSystem_Actions inputActions;
    private Vector2 playerInput;

    private Player player;

    private void Awake()
    {
        playerMovement = GetComponent<IMovable>();
        groundDetector = GetComponent<IGroundDetector>();

        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Move.canceled += Move;

        player = new Player(3, true);
    }

    private void FixedUpdate()
    {
        playerMovement.Move(playerInput);
    }

    private void Move(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();
    }    
}
