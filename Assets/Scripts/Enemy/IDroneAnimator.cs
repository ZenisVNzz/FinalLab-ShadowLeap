using UnityEngine;

public enum DroneAnimationState
{
    Drone_Idle,
    Drone_Dead
}

public interface IDroneAnimator
{
    void Play(DroneAnimationState state);
}
