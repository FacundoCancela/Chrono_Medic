using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                Debug.Log("Kunai colision");
                enemyController.GetDamaged(damage);
                Destroy(gameObject);
            }
        }
    }
}
