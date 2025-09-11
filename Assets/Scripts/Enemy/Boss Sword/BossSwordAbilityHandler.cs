using UnityEngine;

public class BossSwordAbilityHandler : AbilityDependency, IAbility
{
    private BossSwordMove bossSwordMove;
    private BossSwordAttack1 bossSwordAttack1;

    public override void Initialize(AbilityManager abilityManager)
    {
        base.Initialize(abilityManager);
        bossSwordMove = abilityManager.GetAbility<BossSwordMove>();
        bossSwordAttack1 = abilityManager.GetAbility<BossSwordAttack1>();
    }

    public void ProcessAbility()
    {
        if (bossSwordMove.isCloseToPLayer)
        {
            bossSwordAttack1.ProcessAbility();
        }
    }
}
