using UnityEngine;

public class DroneAnimation : MonoBehaviour, IAnimator, IDroneAnimator
{
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Play(DroneAnimationState state)
    {
        _animator.Play(state.ToString());
    }

    public void Play(System.Enum state)
    {
        Play((DroneAnimationState)state);
    }
}
