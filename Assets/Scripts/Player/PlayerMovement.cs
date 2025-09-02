using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 20f;  

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();      
    }

    public void Move(Vector2 input)
    {
        rb.linearVelocityX = Mathf.MoveTowards(rb.linearVelocityX, input.x * moveSpeed, (Mathf.Abs(input.x) > 0.1f ? acceleration : deceleration) * Time.fixedDeltaTime);
    }
  
}
