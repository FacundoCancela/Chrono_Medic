using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalWeapon : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public Transform playerController;
    [SerializeField] public Transform weapon;
    private ExperienceManager experienceManager;

    private void Awake()
    {
        experienceManager = FindAnyObjectByType<ExperienceManager>();
    }


    public void Update()
    {
        weapon.RotateAround(playerController.position, new Vector3(0,0,1), experienceManager.orbitalSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.GetDamaged(playerStats.damageMultiplier * experienceManager.extraOrbitalDamage);
            }
        }
    }


}
