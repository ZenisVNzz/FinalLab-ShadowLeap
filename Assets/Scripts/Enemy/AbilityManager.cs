using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    Passive,
    Action
}

public class AbilityManager
{
    private List<IAbility> PassiveAbilities = new List<IAbility>();
    private List<IAbility> ActionAbilities = new List<IAbility>();

    public void AddAbility(IAbility ability, AbilityType abilityType)
    {
        if (abilityType == AbilityType.Passive)
        {
            PassiveAbilities.Add(ability);
        }
        else if (abilityType == AbilityType.Action)
        {
            ActionAbilities.Add(ability);
        }

        if (ability is AbilityDependency abilityDependency)
        {
            abilityDependency.Initialize(this);
        }
    }

    public T GetAbility<T>() where T : class, IAbility
    {
        foreach (var ability in PassiveAbilities)
        {
            if (ability is T typedAbility)
            {
                return typedAbility;
            }
        }
        foreach (var ability in ActionAbilities)
        {
            if (ability is T typedAbility)
            {
                return typedAbility;
            }
        }
        return null;
    }

    public void ProcessAbilities()
    {
        foreach (var ability in PassiveAbilities)
        {
            ability.ProcessAbility();
        }
    }
}
