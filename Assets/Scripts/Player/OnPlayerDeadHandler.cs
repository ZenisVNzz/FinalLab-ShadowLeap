using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDeadHandler : MonoBehaviour
{
    [SerializeField] private float respawnDelay = 2f;
    private Vector2 checkpoint;

    private List<GameObject> enemiesInMap = new List<GameObject>();

    private void Start()
    {
        EventManager.instance.Register("OnPlayerDead", Respawn);
        GetEnemiesInMap();
    }

    private void GetEnemiesInMap()
    {
        enemiesInMap.Clear();
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesInMap.AddRange(allEnemies);
    }

    private void RespawnEnemies()
    {
        foreach (GameObject enemy in enemiesInMap)
        {
            enemy.GetComponent<Enemy>().SetActiveAgain();
        }
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        transform.position = checkpoint;
        GetComponent<PlayerController>().enabled = true;

        RespawnEnemies();
    }
}
