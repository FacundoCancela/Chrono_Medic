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
        bossWeapon.Attack(targetDir);
    }

    public void EnemyDeath()
    {
        GameDataController.Instance.IncreaseMoney(enemyStats.moneyDroped);
        DropManager.Instance.DropSomething(transform.position);
        WaveManager.Instance.OnEnemyKilled();
        experiencePoint.ExperienceDrop(enemyStats.experienceDropped);
        Destroy(gameObject);

        if (bossWeapon.bossType == BossWeapon.BossType.AmmitAndAnubis)
        {
            EnemySpawner.Instance.SpawnAnubisBoss(transform.position);
            return;
        }

        WaveManager.Instance.EndBossBattle();
    }
}
