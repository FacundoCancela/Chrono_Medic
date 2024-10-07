using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModel : Actor
{
    [SerializeField] private BossWeapon bossWeapon;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private ExperiencePoint experiencePoint;
    [SerializeField] private GameObject experiencePrefab;

    public void Shoot(Vector2 targetDir)
    {
        if (bossWeapon.CanUseSpecialAttack)
        {
            bossWeapon.SpecialAttack(targetDir);
        }
        else if (bossWeapon.CanUseWeapon)
        {
            bossWeapon.FireWeapon(targetDir);
        }
    }

    public void EnemyDeath()
    {
        WaveManager.Instance.EndBossBattle();
        GameDataController.Instance.IncreaseMoney(enemyStats.moneyDroped);
        DropManager.Instance.DropSomething(transform.position);
        WaveManager.Instance.OnEnemyKilled();
        experiencePoint.ExperienceDrop(enemyStats.experienceDropped);
        Destroy(gameObject);
    }
}
