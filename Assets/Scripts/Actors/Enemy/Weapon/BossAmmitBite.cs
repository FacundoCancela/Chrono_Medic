using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAmmitBite : MonoBehaviour
{
    public EnemyStats enemyStats;

    private int damage;

    private void Awake()
    {
        damage = enemyStats.damage;
    }

    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.GetDamaged(damage);
            }
        }
    }
}
