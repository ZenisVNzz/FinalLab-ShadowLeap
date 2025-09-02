using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 20f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = 1f;
    [SerializeField] private float fallAcceleration = 2.5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();      
    }

    public void Move(Vector2 input)
    {
        rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, input.x * moveSpeed, (Mathf.Abs(input.x) > 0.1f ? acceleration : deceleration) * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void FallAccelaration()
    {
        rb.gravityScale += fallAcceleration * Time.deltaTime;
    }

    public void ResetGravity()
    {
        rb.gravityScale = gravity;
    }

    public Vector2 GetVelocity()
    {
        return rb.linearVelocity;
    }
}
