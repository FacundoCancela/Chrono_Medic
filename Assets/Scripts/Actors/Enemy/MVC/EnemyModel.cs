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

    private void Update()
    {
        HandleCooldown();
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
        GameDataController.Instance.IncreaseMoney(enemyStats.moneyDroped);
        DropManager.Instance.DropSomething(transform.position, DropType.Enemy);
        WaveManager.Instance.OnEnemyKilled();
        experiencePoint.ExperienceDrop(enemyStats.experienceDropped);
    }

}
