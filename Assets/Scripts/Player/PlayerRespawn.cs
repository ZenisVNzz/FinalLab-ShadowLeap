using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 2f;
    private Vector2 checkpoint;

    private void Start()
    {
        EventManager.instance.Register("OnPlayerDead", Respawn);
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }

    private void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        transform.position = checkpoint;
        GetComponent<PlayerController>().enabled = true;
    }
}
