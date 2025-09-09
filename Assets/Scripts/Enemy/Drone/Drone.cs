using UnityEngine;

public class Drone : Enemy
{
    protected override void Start()
    {
        base.Start();
        abilityManager.AddAbility(new PlayerDetector(transform, 10f), AbilityType.Passive);
        abilityManager.AddAbility(new Shoot(transform, 1, 2f, PlayShootAnimation), AbilityType.Action);

        animator.Play(DroneAnimationState.Drone_Idle);
    }

    private void Update()
    {
        if (isDead) return;
        abilityManager.ProcessAbilities();
    }

    private void PlayShootAnimation()
    {
        animator.Play(DroneAnimationState.Drone_Shoot);
    }

    protected override void OnDead()
    {
        base.OnDead();
        animator.Play(DroneAnimationState.Drone_Dead);
    }

    public override void SetActiveAgain()
    {
        base.SetActiveAgain();
    }
}
