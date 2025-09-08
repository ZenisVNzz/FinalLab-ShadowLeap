using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimator
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerAnimationState currentState = PlayerAnimationState.Player_Idle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Play(PlayerAnimationState state)
    {
        if (currentState == state) return;

        if (!IsAnimationFinished(currentState.ToString()) && IsNonLoop(currentState))
        {
            return;
        }

        animator.Play(state.ToString());
        currentState = state;
    }

    private bool IsAnimationFinished(string animName)
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(animName) && !stateInfo.loop)
        {
            return stateInfo.normalizedTime >= 1f;
        }
        return false;
    }

    private bool IsNonLoop(PlayerAnimationState state)
    {
        return state == PlayerAnimationState.Player_Attack
            || state == PlayerAnimationState.Player_Hurt
            || state == PlayerAnimationState.Player_Death;
    }

    public void Flip(bool x)
    {
        spriteRenderer.flipX = x;
    }
}
