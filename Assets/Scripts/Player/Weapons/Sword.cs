using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damage = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                // Aquí llamamos al método GetDamaged del EnemyController
                Debug.Log("Sword colision");
                enemyController.GetDamaged(damage);
            }
        }
    }

}
