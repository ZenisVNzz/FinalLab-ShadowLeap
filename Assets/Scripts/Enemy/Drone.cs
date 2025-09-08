using UnityEngine;

public class Drone : Enemy
{
    protected override void Start()
    {
        base.Start();
        abilityManager.AddAbility(new PlayerDetector(transform, 10f), AbilityType.Passive);
        abilityManager.AddAbility(new Shoot(transform, 1, 2f), AbilityType.Action);

        animator.Play(DroneAnimationState.Drone_Idle);
    }

    private void Update()
    {
        abilityManager.ProcessAbilities();
    }

    protected override void OnDead()
    {
        animator.Play(DroneAnimationState.Drone_Dead);
        Destroy(gameObject, 1f);    
    }
}
