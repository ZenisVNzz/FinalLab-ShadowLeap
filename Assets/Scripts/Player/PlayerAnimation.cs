using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimator
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void PlayIdle()
    {
        animator.Play("Player_Idle");
    }

    public void PlayMove()
    {
        animator.Play("Player_Move");
    }

    public void PlayDash()
    {
        animator.Play("Player_Dash");
    }

    public void PlayJump()
    {
        animator.Play("Player_Jump");
    }

    public void PlayFall()
    {
        animator.Play("Player_Fall");
    }

    public void PlayAttack()
    {
        animator.Play("Player_Attack");
    }

    public void Flip(bool x)
    {
        spriteRenderer.flipX = x;
    }
}
