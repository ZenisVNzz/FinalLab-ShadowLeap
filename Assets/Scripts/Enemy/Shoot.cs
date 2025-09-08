using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Shoot : AbilityDependency, IAbility
{
    private int bulletDamage = 1;
    private float shootCooldown = 3f;
    private float lastShootTime = -Mathf.Infinity;
    private Transform player = GameObject.FindGameObjectWithTag("Player").transform;
    private Transform transform;
    private GameObject bulletPrefab;

    private PlayerDetector playerDetector;

    public Shoot(Transform transform, int damage, float shootCooldown)
    {
        this.transform = transform;
        this.bulletDamage = damage;
        this.shootCooldown = shootCooldown;
        this.bulletPrefab = Addressables.LoadAssetAsync<GameObject>("Bullet").WaitForCompletion();
    }

    public override void Initialize(AbilityManager abilityManager)
    {
        base.Initialize(abilityManager);
        playerDetector = abilityManager.GetAbility<PlayerDetector>();
        playerDetector.OnPlayerDetected += ProcessAbility;
    }

    public void ProcessAbility()
    {
        if (Time.time - lastShootTime < shootCooldown) return;
        lastShootTime = Time.time;

        if (playerDetector != null)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Debug.Log("Shoot bullet!");
        }
    }
}
