using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public Transform playerController;
    [SerializeField] public ExperienceManager experienceManager;

    private void Awake()
    {
        experienceManager = FindAnyObjectByType<ExperienceManager>();
        playerController = FindAnyObjectByType<PlayerController>().transform;
    }

    public void Update()
    {
        transform.position = playerController.position;
        transform.RotateAround(playerController.position, new Vector3(0, 0, -1), experienceManager.curveSwordSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IEnemyController enemyController = collision.gameObject.GetComponent<IEnemyController>();
            ExperienceManager experienceManager = FindAnyObjectByType<ExperienceManager>();
            if (enemyController != null)
            {
                enemyController.GetDamaged(playerStats.damageMultiplier * experienceManager.extraCurveSwordDamage);
            }
        }
    }
}
