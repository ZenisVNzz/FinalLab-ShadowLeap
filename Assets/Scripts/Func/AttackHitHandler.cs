using UnityEngine;

public enum Faction { Player, Enemy}

public class AttackHitHandler : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private Faction faction;

    public void Initialized(int damage, Faction faction)
    {
        this.damage = damage;
        this.faction = faction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IAttackable>() != null && collision.gameObject.tag != faction.ToString())
        {
            collision.gameObject.GetComponent<IAttackable>().TakeDamage(damage);

            if (faction.ToString() == "Player") VFXManager.Instance.Initialize(100003, collision.transform.position);

            CameraShake.instance.ShakeCamera();
        }
    }
}
