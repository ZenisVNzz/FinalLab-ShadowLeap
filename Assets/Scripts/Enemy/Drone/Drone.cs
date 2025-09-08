using UnityEngine;

public class Drone : Enemy
{
    private bool isDead = false;

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
        animator.Play(DroneAnimationState.Drone_Dead);
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);    
    }
}
