using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Shoot : AbilityDependency, IAbility
{
    private int bulletDamage = 1;
    private float shootCooldown = 2f;
    private float lastShootTime = -Mathf.Infinity;
    private Transform player = GameObject.FindGameObjectWithTag("Player").transform;
    private Transform transform;
    private GameObject bulletPrefab;

    private Action animationCall;
    private PlayerDetector playerDetector;

    public Shoot(Transform transform, int damage, float shootCooldown, Action animationCallBack)
    {
        this.transform = transform;
        this.bulletDamage = damage;
        this.shootCooldown = shootCooldown;
        this.animationCall = animationCallBack;
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
            Vector2 shootDir = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position, rotation);

            Bullet bulletRuntime = bullet.GetComponent<Bullet>();
            bulletRuntime.Init(shootDir);
            animationCall?.Invoke();
        }
    }
}
