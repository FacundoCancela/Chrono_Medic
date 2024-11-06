using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DropManager;

public class BossModel : Actor
{
    [SerializeField] public BossWeapon bossWeapon;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private ExperiencePoint experiencePoint;
    [SerializeField] private GameObject experiencePrefab;

    [SerializeField] private GameObject objectToSpawnOnDeath;

    public override void Move(Vector2 dir)
    {
        dir *= enemyStats.movementSpeed;
        base.Move(dir);
    }

    public void Shoot(Vector2 targetDir, Rigidbody2D targetRigidbody)
    {
        bossWeapon.Attack(targetDir, targetRigidbody);
    }

    public void EnemyDeath()
    {
        // Incrementa dinero y experiencia, y gestiona el drop del enemigo
        GameDataController.Instance.IncreaseMoney(enemyStats.moneyDroped);
        DropManager.Instance.DropSomething(transform.position, DropType.Boss);
        WaveManager.Instance.OnEnemyKilled();
        experiencePoint.ExperienceDrop(enemyStats.experienceDropped);

        // Instanciar el objeto y ajustar la escala para que sea la misma que la del jefe
        if (objectToSpawnOnDeath != null)
        {
            GameObject spawnedObject = Instantiate(objectToSpawnOnDeath, transform.position, Quaternion.identity);
            spawnedObject.transform.localScale = transform.localScale; // Ajustar la escala
        }

        // Condición especial para Ammit y Anubis
        if (bossWeapon.bossType == BossWeapon.BossType.AmmitAndAnubis)
        {
            EnemySpawner.Instance.SpawnAnubisBoss(transform.position);
            Destroy(gameObject);
            return;
        }

        if (bossWeapon.bossType == BossWeapon.BossType.Anubis)
        {
            Destroy(gameObject);
            return;
        }

        // Finalizar la batalla de jefe y destruir al jefe
        WaveManager.Instance.EndBossBattle();
        Destroy(gameObject);
    }
}
