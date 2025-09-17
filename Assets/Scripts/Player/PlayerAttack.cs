using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.AudioSettings;

public class PlayerAttack : MonoBehaviour, IPlayerAttack
{
    [Header("Attack Settings")]
    [SerializeField] private float coolddownTime = 0.5f;
    [SerializeField] private float detectRange = 10f;
    private float cooldownTimer;

    private bool isAttacking;

    public void Attack()
    {
        if (cooldownTimer > 0) return;

        Vector2 attackDirection = Vector2.zero;
        Vector2 playerPos = transform.GetChild(0).transform.position;

#if UNITY_ANDROID || UNITY_IOS
        Transform nearestEnemy = FindNearestEnemy(playerPos);
        if (nearestEnemy != null)
        {
            attackDirection = ((Vector2)nearestEnemy.position - playerPos).normalized;
        }
        else
        {
            return;
        }
#elif UNITY_WEBGL

        if (SystemInfo.operatingSystem.Contains("Android") || SystemInfo.operatingSystem.Contains("iOS"))
        {
            Transform nearestEnemy = FindNearestEnemy(playerPos);
            if (nearestEnemy != null)
            {
                attackDirection = ((Vector2)nearestEnemy.position - playerPos).normalized;
            }
            else
            {
                return;
            }
        }
        else
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
            attackDirection = (mouseWorld - playerPos).normalized;
        }
#else
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        attackDirection = (mouseWorld - playerPos).normalized;
#endif

        cooldownTimer = coolddownTime;
        isAttacking = true;

        Vector2 spawnPos = playerPos + attackDirection * 1f;
        Quaternion rot = Quaternion.FromToRotation(Vector3.right, attackDirection);

        SFXManager.instance.PlaySFX("200004");
        VFXManager.Instance.Initialize(100002, spawnPos, rot);
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            isAttacking = false;
        }
    }

    private Transform FindNearestEnemy(Vector2 playerPos)
    {
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var e in enemies)
        {
            float dist = Vector2.Distance(playerPos, e.transform.position);
            if (dist < minDist && dist <= detectRange)
            {
                minDist = dist;
                nearest = e.transform;
            }
        }

        return nearest;
    }
}