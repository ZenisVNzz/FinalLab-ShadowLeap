using UnityEngine;

public class Spike : MonoBehaviour, IDamageDealer
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        IAttackable attackable = collision.gameObject.GetComponent<IAttackable>();
        if (attackable != null)
        {
            Attack(attackable);
        }
    }

    public void Attack(IAttackable target)
    {
        target.TakeDamage(3);
    }
}
