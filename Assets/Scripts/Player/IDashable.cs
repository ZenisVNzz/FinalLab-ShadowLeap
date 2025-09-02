using UnityEngine;

public interface IDashable
{
    bool CanDash();
    void Dash(Vector2 direction);
    void ResetDash();
    void DashHandle();
}
