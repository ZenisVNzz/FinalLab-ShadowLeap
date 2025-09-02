using UnityEngine;

public interface IPlayerAnimator
{
    void PlayIdle();
    void PlayMove();
    void PlayJump();
    void PlayFall();
    void Flip(bool x);
}
