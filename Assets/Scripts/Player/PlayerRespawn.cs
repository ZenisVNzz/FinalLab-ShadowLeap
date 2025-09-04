using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
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
        transform.position = checkpoint;
        GetComponent<PlayerController>().enabled = true;
    }
}
