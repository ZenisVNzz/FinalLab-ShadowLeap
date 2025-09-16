using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IPlayerAttack
{
    [Header("Attack Settings")]
    [SerializeField] private float coolddownTime = 0.5f;
    private float cooldownTimer;

    private bool isAttacking;

    public void Attack()
    {
        if (cooldownTimer <= 0)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            cooldownTimer = coolddownTime;
            isAttacking = true;
            Vector2 playerTransform = transform.GetChild(0).transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 attackDirection = (mousePosition - playerTransform).normalized;
            Vector2 spawnPos = playerTransform + attackDirection * 1f;

            Quaternion rot = Quaternion.FromToRotation(Vector3.right, attackDirection);

            SFXManager.instance.PlaySFX("200004");
            VFXManager.Instance.Initialize(100002, spawnPos, rot);
        }
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
}