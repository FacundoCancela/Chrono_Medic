using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : Actor
{

    [SerializeField] private EnemyWeapon enemyWeapon;
    [SerializeField] private EnemyStats enemyStats;

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
    }

}
