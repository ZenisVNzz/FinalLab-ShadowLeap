using UnityEngine;

public enum BossSwordAnimationState
{
    BossSword_Run,
    BossSword_Idle,
    BossSword_Hurt,
    BossSword_Dead,
    BossSword_Attack1,
    BossSword_Attack2,
    BossSword_Attack3,
    BossSword_Attack4
}

public interface IBossSwordAnimator
{
    void Play(BossSwordAnimationState state);
}
