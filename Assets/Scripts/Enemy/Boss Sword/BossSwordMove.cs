using UnityEngine;
using UnityEngine.EventSystems;

public class BossSwordMove : IAbility, IMovable
{
    private Rigidbody2D rb;
    private Transform transform;
    private Transform targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
    private SpriteRenderer spriteRenderer;
    private float speed;
    private float stopDistance = 2.2f;
    private bool canMove = false;
    private BossSwordState bossSwordState;
    public bool isCloseToPLayer => Vector2.Distance(transform.position, targetPosition.position) <= stopDistance;

    public BossSwordMove(BossSwordState bossSwordState, SpriteRenderer spriteRenderer, Transform transform, Rigidbody2D rb, float speed)
    {
        this.bossSwordState = bossSwordState;
        this.spriteRenderer = spriteRenderer;
        this.transform = transform;
        this.rb = rb;
        this.speed = speed;
    }

    public void Move(Vector2 targetPosition)
    {
        if (canMove)
        {
            Vector2 groundTarget = new Vector2(targetPosition.x, transform.position.y);

            Vector2 direction = (groundTarget - (Vector2)transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }     
    }

    public void ProcessAbility()
    {
        float distance = Vector2.Distance(transform.position, targetPosition.position);

        if (targetPosition.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (distance > stopDistance)
        {
            Move(targetPosition.position);
            bossSwordState.ChangeState(BossSwordAnimationState.BossSword_Run);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            bossSwordState.ChangeState(BossSwordAnimationState.BossSword_Idle);
        }
    }

    public void SetMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
