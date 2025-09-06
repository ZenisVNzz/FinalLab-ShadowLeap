using UnityEngine;

public enum PlayerAnimationState
{
    Player_Idle,
    Player_Move,
    Player_Dash,
    Player_Jump,
    Player_Fall,
    Player_Attack,
    Player_Hurt,
    Player_Death
}

public interface IPlayerAnimator
{
    void Play(PlayerAnimationState state);
    void Flip(bool x);
}
