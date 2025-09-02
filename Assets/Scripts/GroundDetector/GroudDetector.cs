using UnityEngine;

public class GroudDetector : MonoBehaviour, IGroundDetector
{
    [Header("GroundCheck Settings")]
    public Transform groundCheck;
    [SerializeField] private float checkDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);
    }
}
