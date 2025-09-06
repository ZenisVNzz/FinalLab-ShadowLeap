using UnityEngine;

public abstract class Enemy : MonoBehaviour, IAttackable
{
    [SerializeField] protected int health = 1;
    protected IAnimator animator;

    protected void Start()
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
