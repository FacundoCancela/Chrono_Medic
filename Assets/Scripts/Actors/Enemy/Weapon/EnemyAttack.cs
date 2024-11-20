using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyStats enemyStats;

    private int damage;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.GetDamaged(damage);
                Debug.Log("intento hacer: " + damage);
                Destroy(gameObject);
            }
        }
    }
}
