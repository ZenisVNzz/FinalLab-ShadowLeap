using UnityEngine;

public abstract class AbilityDependency
{
    protected AbilityManager abilityManager;

    public virtual void Initialize(AbilityManager abilityManager)
    {
        this.abilityManager = abilityManager;
    }
}
