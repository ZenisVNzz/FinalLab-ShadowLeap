using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IAttackable
{
    [SerializeField] protected int health = 1;
    protected AbilityManager abilityManager = new AbilityManager();
    protected IAnimator animator;

    protected virtual void Start()
    {
        animator = GetComponent<IAnimator>();
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDead();
        }
    }

    protected virtual void OnDead()
    {
    }
}
