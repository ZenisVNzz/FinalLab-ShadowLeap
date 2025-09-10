using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Action OnCheckpointReach;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 checkpointPosition = new Vector2(transform.position.x, transform.position.y);
            Debug.Log("Checkpoint reached at position: " + checkpointPosition);
            OnCheckpointReach?.Invoke();
            collision.GetComponent<OnPlayerDeadHandler>().SetCheckpoint(checkpointPosition);
        }
    }
}
