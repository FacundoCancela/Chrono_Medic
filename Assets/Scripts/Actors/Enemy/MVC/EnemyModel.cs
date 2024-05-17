using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : Actor
{

    [SerializeField] private EnemyWeapon enemyWeapon;

    public void Shoot(Vector2 targetDir)
    {
         if(enemyWeapon.CanUseWeapon)
         {
            enemyWeapon.FireWeapon(targetDir);
         }
    }

    public void EnemyDeath()
    {
        Debug.Log("Me mori");
        WaveManager.Instance.OnEnemyKilled();
    }

}
