using UnityEngine;

public interface IPlayerAnimator
{
    void PlayIdle();
    void PlayMove();
    void PlayDash();
    void PlayJump();
    void PlayFall();
    void PlayAttack();
    void Flip(bool x);
}
