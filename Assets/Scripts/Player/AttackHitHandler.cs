using UnityEngine;

public class AttackHitHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IAttackable>() != null && collision.gameObject.tag != "Player")
        {
            collision.gameObject.GetComponent<IAttackable>().TakeDamage(1);
        }
    }
}
