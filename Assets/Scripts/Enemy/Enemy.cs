using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IAttackable
{
    [SerializeField] protected int health = 1;
    protected AbilityManager abilityManager = new AbilityManager();
    protected IAnimator animator;

    protected bool isDead = false;

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
        isDead = true;
        CameraShake.instance.ShakeCamera();
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(SetUnactive());
    }

    protected virtual IEnumerator SetUnactive()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public virtual void SetActiveAgain()
    {
        isDead = false;
        health = 1;  
        gameObject.SetActive(true);
        GetComponent<Collider2D>().enabled = true;
        animator.Play(DroneAnimationState.Drone_Idle);
    }
}
