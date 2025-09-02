using UnityEngine;

public interface IJumpable
{
    void Jump();
    void FallAccelaration();
    void ResetGravity();
    Vector2 GetVelocity();
}
