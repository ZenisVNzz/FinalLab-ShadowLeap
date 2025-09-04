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

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private int dashCountMax = 2;

    private GhostTrail ghostTrail;
    private int dashCount;
    private bool isDashing;
    private float dashTimeCounter;
    private Vector2 dashDirection;
    

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ghostTrail = GetComponentInChildren<GhostTrail>();
        EventManager.instance.Register("OnPlayerDead", ZeroGravity);
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

    public bool CanDash()
    {
        return dashCount < dashCountMax && !isDashing;
    }

    public void Dash(Vector2 input)
    {
        isDashing = true;
        dashCount++;
        dashTimeCounter = dashTime;
        dashDirection = input.normalized;
        if (dashDirection == Vector2.zero)
        {
            float facing = GetComponentInChildren<SpriteRenderer>().flipX ? -1f : 1f;
            dashDirection = new Vector2(facing, 0);
        }
        rb.gravityScale = 0;
        rb.linearVelocity = dashDirection * dashSpeed;
        ghostTrail.StartTrail();
    }

    public void DashHandle()
    {
        if (isDashing)
        {
            if (dashTimeCounter > 0)
            {
                rb.linearVelocity = dashDirection * dashSpeed;
                dashTimeCounter -= Time.deltaTime;
            }
            else
            {    
                isDashing = false;
                ghostTrail.StopTrail();
                ResetGravity();
            }
        }
    }

    public void ResetDash()
    {
        dashCount = 0;
    }

    public void ResetGravity()
    {
        rb.gravityScale = gravity;
    }

    private void ZeroGravity()
    {
        rb.gravityScale = 0;
        rb.linearVelocityY = 0;
    }

    public Vector2 GetVelocity()
    {
        return rb.linearVelocity;
    }
}
