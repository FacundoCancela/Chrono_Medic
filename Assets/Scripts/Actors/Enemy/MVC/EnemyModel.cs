using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyModel : Actor
{
    //public Animator animator;

    [SerializeField] private EnemyWeapon enemyWeapon;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private ExperiencePoint experiencePoint;
    [SerializeField] private GameObject experiencePrefab;

    public void Shoot(Vector2 targetDir)
    {
         if(enemyWeapon.CanUseWeapon)
         {
            enemyWeapon.FireWeapon(targetDir);
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
