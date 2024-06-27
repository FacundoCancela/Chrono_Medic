using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;

    private Rigidbody2D rb;
    public float boomerangDuration;
    public float boomerangSpeed;
    public float boomerangTurnArroundTime;
    public float boomerangLaunchedTime;
    public bool boomerangInverted = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        LaunchBoomerang(transform.right);
    }

    private void Update()
    {
        boomerangLaunchedTime += Time.deltaTime;
        if(boomerangLaunchedTime > boomerangTurnArroundTime && !boomerangInverted)
        {
            InvertBoomerangDirection();
        }
    }

    public void LaunchBoomerang(Vector3 dir)
    {
        rb.velocity = dir * boomerangSpeed;
        boomerangLaunchedTime = 0f;
        boomerangInverted = false;
    }

    public void InvertBoomerangDirection()
    {
        rb.velocity = -rb.velocity;
        boomerangInverted = true;
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            ExperienceManager experienceManager = FindAnyObjectByType<ExperienceManager>();
            if (enemyController != null)
            {
                enemyController.GetDamaged(playerStats.projectileDamage * playerStats.damageMultiplier * experienceManager.extraRangedDamage);
            }
        }
    }


}
