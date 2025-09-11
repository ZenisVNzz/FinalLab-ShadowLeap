using UnityEngine;

public class BossSwordAttack1 : IAbility
{
    private BossSwordState bossSwordState;
    private GameObject hitBox;
    private int damage;

    public BossSwordAttack1(BossSwordState bossSwordState, int damage)
    {
        this.bossSwordState = bossSwordState;
        this.damage = damage;
        hitBox = GameObject.Find("BossSwordHitBox1");
        hitBox.AddComponent<AttackHitHandler>().Initialized(damage, Faction.Enemy);
    }

    public void ProcessAbility()
    {
        bossSwordState.ChangeState(BossSwordAnimationState.BossSword_Attack1);
    }
}
