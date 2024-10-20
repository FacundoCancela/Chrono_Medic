using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    public void Shoot(Vector2 targetDir)
    {
        if (canUseWeapon)
        {
            enemyWeapon.FireWeapon(targetDir);
            canUseWeapon = false;
        }
    }

    public void EnemyDeath()
    {
        GameDataController.Instance.IncreaseMoney(enemyStats.moneyDroped);
        DropManager.Instance.DropSomething(transform.position);
        WaveManager.Instance.OnEnemyKilled();
        experiencePoint.ExperienceDrop(enemyStats.experienceDropped);
    }

}
