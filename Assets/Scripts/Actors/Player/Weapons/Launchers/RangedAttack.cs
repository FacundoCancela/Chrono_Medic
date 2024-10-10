using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour, IWeapon
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public ExperienceManager experienceManager;
    [SerializeField] public GameObject rangedPrefab;
    [SerializeField] public Transform attackPosition;
    [SerializeField] public float _timeSinceLastRangedAttack = 0f;
    bool _rangedAttackInCooldown = false;

    private void Update()
    {
        if (_rangedAttackInCooldown)
        {
            _timeSinceLastRangedAttack += Time.deltaTime;
        }
        if (experienceManager != null)
        {
            if (_timeSinceLastRangedAttack > experienceManager.rangedCooldown)
            {
                _rangedAttackInCooldown = false;
                _timeSinceLastRangedAttack = 0f;
            }

        }

    }

    public void Attack()
    {
        if (!_rangedAttackInCooldown)
        {
            // Buscar al enemigo más cercano
            EnemyController[] enemies = FindObjectsOfType<EnemyController>();
            EnemyController closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (EnemyController enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            // Si se encontró un enemigo, atacar en su dirección
            if (closestEnemy != null)
            {
                Vector2 direction = (closestEnemy.transform.position - attackPosition.transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                Instantiate(rangedPrefab, attackPosition.transform.position, rotation).GetComponent<Rigidbody2D>().velocity = direction * 10f; // Velocidad del proyectil
            }

            _rangedAttackInCooldown = true;
        }
    }
}
