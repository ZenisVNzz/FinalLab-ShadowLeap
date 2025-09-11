using UnityEngine;

public class BossSwordAttack2 : IAbility
{
    private BossSwordState bossSwordState;
    private GameObject hitBox;

    public BossSwordAttack2(BossSwordState bossSwordState, int damage)
    {
        this.bossSwordState = bossSwordState;
        hitBox = GameObject.Find("BossSwordHitBox2");
        hitBox.AddComponent<AttackHitHandler>().Initialized(damage, Faction.Enemy);
    }

    public void ProcessAbility()
    {
        bossSwordState.ChangeState(BossSwordAnimationState.BossSword_Attack2);
    }
}
