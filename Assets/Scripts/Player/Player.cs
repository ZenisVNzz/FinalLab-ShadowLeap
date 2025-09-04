using System;
using UnityEngine;

[Serializable]
public class Player : IAttackable
{
    public int lives;
    public bool movable;

    public Player(int lives, bool movable)
    {
        this.lives = lives;
        this.movable = movable;
    }

    public void TakeDamage(float damage)
    {
        lives -= (int)damage;
        if (lives <= 0)
        {
            
            Debug.Log("Player is dead.");
        }
    }
}
