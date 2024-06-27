using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveSword : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public Transform playerController;
    [SerializeField] public Transform weapon;

    public float spinSpeed;

    public void Update()
    {
        weapon.RotateAround(playerController.position, new Vector3(0, 0, -1), spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            ExperienceManager experienceManager = FindAnyObjectByType<ExperienceManager>();
            if (enemyController != null)
            {
                enemyController.GetDamaged(playerStats.orbitalDamage * playerStats.damageMultiplier * experienceManager.extraOrbitalDamage);
            }
        }
    }
}