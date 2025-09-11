using UnityEngine;

public class ActiveBoss : MonoBehaviour
{
    [SerializeField] private BossSword bossSword;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossSword.GetComponent<BossSword>().enabled = true;
            Destroy(this);
        }
    }
}
