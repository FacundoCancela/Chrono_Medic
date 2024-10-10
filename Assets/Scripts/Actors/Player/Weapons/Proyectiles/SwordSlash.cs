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
            IEnemyController enemyController = collision.gameObject.GetComponent<IEnemyController>();
            ExperienceManager experienceManager = FindAnyObjectByType<ExperienceManager>();
            if (enemyController != null)
            {
                Debug.Log("damage multiplayer: " + playerStats.damageMultiplier);
                Debug.Log("experience damage multiplayer: " + experienceManager.meleeDamage);
                enemyController.GetDamaged(playerStats.damageMultiplier * experienceManager.meleeDamage);
            }
        }
    }
}
