using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePoint : MonoBehaviour
{
    public int experience;

    private void Awake()
    {
        EnemyModel.OnEnemyDeath += HandleEnemyDeath;
        BossModel.OnBossDeath += HandleEnemyDeath;
    }

    private void OnDestroy()
    {
        EnemyModel.OnEnemyDeath -= HandleEnemyDeath;
        BossModel.OnBossDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(EnemyDeathData deathData)
    {
        experience = deathData.ExperienceDropped;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ExperienceManager experienceManager = FindObjectOfType<ExperienceManager>();
            if (experienceManager != null)
            {
                experienceManager.gainExperience(experience);
                Destroy(gameObject);
            }                         
        }
    }
}
