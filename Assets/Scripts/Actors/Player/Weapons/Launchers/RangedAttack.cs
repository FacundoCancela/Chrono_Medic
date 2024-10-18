using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangedAttack : MonoBehaviour, IWeapon
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public ExperienceManager experienceManager;
    [SerializeField] public GameObject rangedPrefab;
    [SerializeField] public Transform attackPosition;
    [SerializeField] public float _timeSinceLastRangedAttack = 0f;
    bool _rangedAttackInCooldown = false;

    public bool specialAttackMode = false;

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
            if (specialAttackMode)
            {
                SpecialAttack();
            }
            else
            {
                NormalAttack();
            }

            _rangedAttackInCooldown = true;
        }
    }

    private void NormalAttack()
    {
        IEnemyController closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            Vector2 direction = (closestEnemy.transform.position - attackPosition.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Instantiate(rangedPrefab, attackPosition.transform.position, rotation).GetComponent<Rigidbody2D>().velocity = direction * 10f;
        }
    }

    private void SpecialAttack()
    {
        IEnemyController closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            Vector2 enemyDirection = (closestEnemy.transform.position - attackPosition.transform.position).normalized;
            float baseAngle = Mathf.Atan2(enemyDirection.y, enemyDirection.x) * Mathf.Rad2Deg;

            for (int i = 0; i < 4; i++)
            {
                float angle = baseAngle + (i * 90); // Ángulos de 0°, 90°, 180°, 270°
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
                Instantiate(rangedPrefab, attackPosition.transform.position, rotation).GetComponent<Rigidbody2D>().velocity = direction * 10f;
            }
        }
    }

    private IEnemyController FindClosestEnemy()
    {
        IEnemyController[] enemies = FindObjectsOfType<MonoBehaviour>().OfType<IEnemyController>().ToArray();
        IEnemyController closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (IEnemyController enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
