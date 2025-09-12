using UnityEngine;

public class BossSword : Enemy
{
    public BossSwordState bossSwordState;

    protected override void Start()
    {
        base.Start();
        health = 45;
        bossSwordState = new BossSwordState(animator);
        abilityManager.AddAbility(new PlayerDetector(transform, 15f), AbilityType.Passive);
        abilityManager.AddAbility(new BossSwordMove(bossSwordState, GetComponent<SpriteRenderer>(), this.transform, GetComponent<Rigidbody2D>(), 5.5f), AbilityType.Passive);
        abilityManager.AddAbility(new BossSwordDash(GetComponent<Rigidbody2D>(), GetComponent<GhostTrail>(), this), AbilityType.Action);
        abilityManager.AddAbility(new BossSwordDash2(GetComponent<Rigidbody2D>(), GetComponent<GhostTrail>(), this), AbilityType.Action);
        abilityManager.AddAbility(new BossSwordAttack1(bossSwordState, 1), AbilityType.Action);
        abilityManager.AddAbility(new BossSwordAttack2(bossSwordState, 1), AbilityType.Action);
        abilityManager.AddAbility(new BossSwordAttack4(bossSwordState, 1), AbilityType.Action);
        abilityManager.AddAbility(new BossSwordAbilityHandler(this), AbilityType.Passive);
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
        SFXManager.instance.PlaySFX("200009");
        bossSwordState.ChangeState(BossSwordAnimationState.BossSword_Hurt);
    }

    protected override void OnDead()
    {
        base.OnDead();
        bossSwordState.ChangeState(BossSwordAnimationState.BossSword_Dead);
        SFXManager.instance.PlaySFX("200008");
    }

    public override void SetActiveAgain()
    {
        base.SetActiveAgain();
    }
}
