using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 10f;   
    [SerializeField] private float lifeTime = 8f;

    public void Init(Vector2 dir)
    {
        GetComponent<Rigidbody2D>().linearVelocity = dir * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }    

        var target = collision.gameObject.GetComponent<IAttackable>();
        if (target != null)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
