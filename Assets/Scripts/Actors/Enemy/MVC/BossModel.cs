using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModel : Actor
{
    [SerializeField] private BossWeapon bossWeapon;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private ExperiencePoint experiencePoint;
    [SerializeField] private GameObject experiencePrefab;

    [SerializeField] private GameObject objectToSpawnOnDeath;

    public void Shoot(Vector2 targetDir)
    {
        bossWeapon.Attack(targetDir);
    }

    public void EnemyDeath()
    {
        // Incrementa dinero y experiencia, y gestiona el drop del enemigo
        GameDataController.Instance.IncreaseMoney(enemyStats.moneyDroped);
        DropManager.Instance.DropSomething(transform.position);
        WaveManager.Instance.OnEnemyKilled();
        experiencePoint.ExperienceDrop(enemyStats.experienceDropped);

        // Instanciar el objeto justo antes de que el jefe se destruya
        if (objectToSpawnOnDeath != null)
        {
            Instantiate(objectToSpawnOnDeath, transform.position, Quaternion.identity);
        }

        // Condición especial para Ammit y Anubis
        if (bossWeapon.bossType == BossWeapon.BossType.AmmitAndAnubis)
        {
            EnemySpawner.Instance.SpawnAnubisBoss(transform.position);
            return;
        }

        // Finalizar la batalla de jefe y destruir al jefe
        WaveManager.Instance.EndBossBattle();
        Destroy(gameObject);
    }
}