using UnityEngine;

public class GroudDetector : MonoBehaviour, IGroundDetector
{
    [Header("GroundCheck Settings")]
    [SerializeField] private float checkDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private Transform groundCheck;

    private void Awake()
    {
        groundCheck = transform;
    }

    public bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkDistance, groundLayer);
    }
}
