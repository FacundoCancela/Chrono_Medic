using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public Transform playerController;
    [SerializeField] public ExperienceManager experienceManager;

    CurveAttack curveAttack;

    private void Awake()
    {
        experienceManager = FindAnyObjectByType<ExperienceManager>();
        playerController = FindAnyObjectByType<PlayerController>().transform;
        curveAttack = FindAnyObjectByType<CurveAttack>();
    }

    public void Update()
    {
        transform.position = curveAttack.attackPosition.transform.position;
        transform.RotateAround(curveAttack.attackPosition.transform.position, new Vector3(0, 0, -1), experienceManager.curveSwordSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IEnemyController enemyController = collision.gameObject.GetComponent<IEnemyController>();
            ExperienceManager experienceManager = FindAnyObjectByType<ExperienceManager>();
            if (enemyController != null)
            {
                enemyController.GetDamaged(playerStats.damageMultiplier * experienceManager.curveSwordDamage);
            }
        }
    }
}
