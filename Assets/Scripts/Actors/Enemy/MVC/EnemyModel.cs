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

    //public void Awake()
    //{
    //    animator = GetComponent<Animator>();
    //}

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
        WaveManager.Instance.OnEnemyKilled();
        experiencePoint.ExperienceDrop(enemyStats.experienceDropped);
        Instantiate(experiencePrefab, transform.position, Quaternion.identity);
    }

}
