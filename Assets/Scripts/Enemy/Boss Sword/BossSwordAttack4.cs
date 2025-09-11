using UnityEngine;

public class BossSwordAttack4 : IAbility
{
    private BossSwordState bossSwordState;
    private GameObject hitBox;

    public BossSwordAttack4(BossSwordState bossSwordState, int damage)
    {
        this.bossSwordState = bossSwordState;
        hitBox = GameObject.Find("BossSwordHitBox4");
        hitBox.AddComponent<AttackHitHandler>().Initialized(damage, Faction.Enemy);
    }

    public void ProcessAbility()
    {
        bossSwordState.ChangeState(BossSwordAnimationState.BossSword_Attack4);
    }
}
