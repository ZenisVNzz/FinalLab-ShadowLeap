using UnityEngine;

public class Drone : Enemy
{
    protected override void OnDead()
    {
        animator.Play(DroneAnimationState.Drone_Dead);
        Destroy(gameObject, 1f); 
    }
}
