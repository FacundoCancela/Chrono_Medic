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
    [SerializeField] public float extraAttackSpeed = 1f;
    public bool _rangedAttackInCooldown = false;

    public bool specialAttackMode = false;

    private void Update()
    {
        if (_rangedAttackInCooldown)
        {
            _timeSinceLastRangedAttack -= Time.deltaTime;
        }
        if (experienceManager != null)
        {
            float cooldown = experienceManager.rangedCooldown;
            if (specialAttackMode)
            {
                cooldown /= extraAttackSpeed;
            }
            if (_timeSinceLastRangedAttack <= 0f)
            {
                _rangedAttackInCooldown = false;
                _timeSinceLastRangedAttack = cooldown;
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
            FireProjectile(baseAngle);
            float randomUpAngle = Random.Range(0f, 30f);
            FireProjectile(baseAngle + randomUpAngle);
            float randomDownAngle = Random.Range(-30f, 0f);
            FireProjectile(baseAngle + randomDownAngle);
        }
    }

    private void FireProjectile(float angle)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        Instantiate(rangedPrefab, attackPosition.transform.position, rotation).GetComponent<Rigidbody2D>().velocity = direction * 10f;
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
