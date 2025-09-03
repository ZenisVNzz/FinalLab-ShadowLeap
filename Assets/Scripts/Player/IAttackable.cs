using UnityEngine;

public interface IPlayerAttack
{
    void Attack(Vector2 mousePos);
    bool IsAttacking();
}
