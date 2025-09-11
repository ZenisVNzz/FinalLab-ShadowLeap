using UnityEngine;

public class BossSwordAnimation : MonoBehaviour, IAnimator, IBossSwordAnimator
{
    private Animator _animator;

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Play(BossSwordAnimationState state)
    {
        _animator.Play(state.ToString());
    }

    public void Play(System.Enum state)
    {
        Play((BossSwordAnimationState)state);
    }
}
