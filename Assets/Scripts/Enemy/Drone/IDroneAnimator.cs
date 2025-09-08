using UnityEngine;

public enum DroneAnimationState
{
    Drone_Idle,
    Drone_Dead,
    Drone_Shoot
}

public interface IDroneAnimator
{
    void Play(DroneAnimationState state);
}
