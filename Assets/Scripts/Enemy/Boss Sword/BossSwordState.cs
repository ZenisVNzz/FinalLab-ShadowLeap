using Unity.VisualScripting;
using UnityEngine;

public class BossSwordState
{
    private BossSwordAnimationState currentState;
    private IAnimator bossSwordAnimator;
    private Animator animator;

    public BossSwordState(IAnimator animator)
    {
        this.bossSwordAnimator = animator;
        currentState = BossSwordAnimationState.BossSword_Idle;
        animator.Play(currentState);
        this.animator = ((BossSwordAnimation)bossSwordAnimator).GetComponent<Animator>();
    }

    public void ChangeState(BossSwordAnimationState newState)
    {
        if (newState != BossSwordAnimationState.BossSword_Dead)
        {
            if (currentState == newState) return;

            if (!IsAnimationFinished(currentState.ToString()) && IsNonLoop(currentState))
            {
                return;
            }
        }

        bossSwordAnimator.Play(newState);
        currentState = newState;
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

    private bool IsNonLoop(BossSwordAnimationState state)
    {
        return state == BossSwordAnimationState.BossSword_Attack1
            || state == BossSwordAnimationState.BossSword_Attack2
            || state == BossSwordAnimationState.BossSword_Attack3
            || state == BossSwordAnimationState.BossSword_Attack4
            || state == BossSwordAnimationState.BossSword_Hurt
            || state == BossSwordAnimationState.BossSword_Dead;
    }

    public void AnimationHandler()
    {
        if (IsNonLoop(currentState))
        {
            if (IsAnimationFinished(currentState.ToString()))
            {
                if (currentState != BossSwordAnimationState.BossSword_Dead)
                {
                    ChangeState(BossSwordAnimationState.BossSword_Idle);
                }
            }
        }
    }
}
