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

    public void Play(PlayerAnimationState state)
    {
        animator.Play(state.ToString());
    }

    public void Flip(bool x)
    {
        spriteRenderer.flipX = x;
    }
}
