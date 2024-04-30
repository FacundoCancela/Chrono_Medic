using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public float _weaponCooldown = 1f;
    public float _timeSinceLastAttack = 0f;
    public bool _isAttacking = false;
    public GameObject sword;
    public GameObject kunaiPrefab;

    public void Attack()
    {
        MeleeAttack();
        RangeAttack();
    }

    private void MeleeAttack()
    {
        _isAttacking = true;
        _timeSinceLastAttack = 0f;
        sword.SetActive(true);
        StartCoroutine(DeactivateSwordAfterDelay(0.1f));
    }
    private void RangeAttack()
    {
        _isAttacking = true;

        // Obtener todos los objetos EnemyController en la escena
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        // Inicializar variables para el enemigo m�s cercano
        EnemyController closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        // Iterar sobre cada enemigo para encontrar el m�s cercano
        foreach (EnemyController enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        // Si se encontr� un enemigo, lanzar el Kunai hacia �l
        if (closestEnemy != null)
        {
            Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject kunai = Instantiate(kunaiPrefab, transform.position, rotation);
            kunai.GetComponent<Rigidbody2D>().velocity = direction * 10f; // Velocidad del Kunai
        }

        // Finalizar el ataque despu�s de un tiempo de espera
        StartCoroutine(EndRangeAttackAfterDelay(0.1f));
    }


    private IEnumerator EndRangeAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _isAttacking = false;
    }

    private IEnumerator DeactivateSwordAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sword.SetActive(false);
        _isAttacking = false;
    }

}
