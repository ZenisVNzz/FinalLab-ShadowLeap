using UnityEngine;

public class BossSword : Enemy
{
    public BossSwordState bossSwordState;

    protected override void Start()
    {
        base.Start();
        health = 30;
        bossSwordState = new BossSwordState(animator);
        abilityManager.AddAbility(new PlayerDetector(transform, 15f), AbilityType.Passive);
        abilityManager.AddAbility(new BossSwordMove(bossSwordState, this.transform, GetComponent<Rigidbody2D>(), 5.5f), AbilityType.Passive);
    }

    private void Update()
    {
        if (isDead) return;
        bossSwordState.AnimationHandler();
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        abilityManager.ProcessAbilities();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (isDead) return;
        bossSwordState.ChangeState(BossSwordAnimationState.BossSword_Hurt);
    }

    protected override void OnDead()
    {
        base.OnDead();
        bossSwordState.ChangeState(BossSwordAnimationState.BossSword_Dead);
    }

    public override void SetActiveAgain()
    {
        base.SetActiveAgain();
    }
}
