using System;
using UnityEngine;

public class PlayerDetector : IAbility
{
    private float visionRange = 10f;
    private LayerMask mask = LayerMask.GetMask("Player", "Ground");
    private Transform player = GameObject.FindGameObjectWithTag("Player").transform;
    private Transform transform;

    public Action OnPlayerDetected;

    public PlayerDetector(Transform transform, float visionRange)
    {
        this.transform = transform;
        this.visionRange = visionRange;
    }

    public void ProcessAbility()
    {
        Vector2 dirToPlayer = (player.position - transform.position).normalized;
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer, visionRange, mask);

        Debug.DrawRay(transform.position, dirToPlayer * visionRange, Color.red);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            OnPlayerDetected?.Invoke();
            Debug.Log("Player Detected");
        } 
    }
}
