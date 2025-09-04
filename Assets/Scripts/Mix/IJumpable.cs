using UnityEngine;

public interface IJumpable
{
    void Jump();
    void FallAccelaration();
    void ResetGravity();
    bool CanJump();
    void JumpBufferCounter(bool isJump);
    void CoyoteTimeCounter(bool isGrounded);
    Vector2 GetVelocity();
}
