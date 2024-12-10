using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static DropManager;

public class EnemyModel : Actor
{
    //public Animator animator;

    [SerializeField] public EnemyWeapon enemyWeapon;
    [SerializeField] public EnemyStats enemyStats;
    [SerializeField] private ExperiencePoint experiencePoint;
    [SerializeField] private GameObject experiencePrefab;

    private float fireRateTimer = 0.0f;
    public bool canUseWeapon = true;

    public static event Action<EnemyDeathData> OnEnemyDeath;

    private void Update()
    {
        HandleCooldown();
    }

    private void OnDestroy()
    {
        EnemyDeath();
    }

    private void HandleCooldown()
    {
        if (!canUseWeapon)
        {
            fireRateTimer += Time.deltaTime;

            if (fireRateTimer >= enemyStats.attackCooldown)
            {
                canUseWeapon = true;
                fireRateTimer = 0.0f;
            }
        }
    }

    public override void Move(Vector2 dir)
    {
        dir *= enemyStats.movementSpeed;
        base.Move(dir);
    }

    public void Shoot(Vector2 targetDir, Rigidbody2D targetRigidbody)
    {
        if (canUseWeapon)
        {
            enemyWeapon.FireWeapon(targetDir, targetRigidbody);
            canUseWeapon = false;
        }
    }

    public void EnemyDeath()
    {
        var deathData = new EnemyDeathData
        {
            MoneyDropped = enemyStats.moneyDroped,
            ExperienceDropped = enemyStats.experienceDropped
        };

        OnEnemyDeath?.Invoke(deathData);
        RequestDrop(transform.position, DropType.Enemy);
    }
}

public class EnemyDeathData
{
    public int MoneyDropped { get; set; }
    public int ExperienceDropped { get; set; }
}
