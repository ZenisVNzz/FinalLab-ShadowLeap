using System;
using UnityEngine;

[Serializable]
public class Player
{
    public int lives;
    public bool movable;

    public Player(int lives, bool movable)
    {
        this.lives = lives;
        this.movable = movable;
    }
}
