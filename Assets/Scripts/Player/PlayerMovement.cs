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
    [SerializeField] private float jumpBuffertime = 0.1f;
    [SerializeField] private float coyoteTime = 0.1f;

    private float jumpBufferCounter;
    private float coyoteTimeCounter;

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
        ResetGravity();
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpBufferCounter = 0;
    }

    public bool CanJump()
    {
        return coyoteTimeCounter > 0;
    }

    public void JumpBufferCounter(bool isJump)
    {
        if (isJump)
        {
            jumpBufferCounter = jumpBuffertime;
        }
        else if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    public void CoyoteTimeCounter(bool isGrounded)
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else if (coyoteTimeCounter > 0)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            Jump();
        }
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
