using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    public enum AttackType
    {
        Normal,
        Special
    }

    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public AttackType attackType;
    [SerializeField] public int specialDamageMultiplier = 5;
    [SerializeField] public float specialAttackDuration = 10f;
    [SerializeField] public float speed = 5f;

    private void Start()
    {
        if (attackType == AttackType.Normal)
        {
            Destroy(gameObject, 0.1f);
        }
        else if (attackType == AttackType.Special)
        {
            Destroy(gameObject, specialAttackDuration);
        }
    }
    private void Update()
    {
        if (attackType == AttackType.Special)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IEnemyController enemyController = collision.gameObject.GetComponent<IEnemyController>();
            ExperienceManager experienceManager = FindAnyObjectByType<ExperienceManager>();
            if (enemyController != null)
            {
                int damageMultiplier = attackType == AttackType.Normal ? 1 : specialDamageMultiplier;
                enemyController.GetDamaged(playerStats.damageMultiplier * experienceManager.meleeDamage * damageMultiplier);
            }
        }
    }
}