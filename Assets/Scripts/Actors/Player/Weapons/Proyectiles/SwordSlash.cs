using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;

    private void Start()
    {
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            ExperienceManager experienceManager = FindAnyObjectByType<ExperienceManager>();
            if (enemyController != null)
            {
                enemyController.GetDamaged(playerStats.damageMultiplier * experienceManager.extraMeleeDamage);
            }
        }
    }
}
