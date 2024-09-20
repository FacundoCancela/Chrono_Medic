using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public EnemyStats enemyStats;
    public bool CanUseWeapon => canUseWeapon;
    public bool CanUseSpecialAttack => canUseSpecialAttack;

    private bool canUseWeapon = true;
    private bool canUseSpecialAttack = false;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject specialBulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletSpeed = 10.0f;
    [SerializeField] private float fireRate;
    [SerializeField] private float specialAttackCooldown = 10.0f;

    private float fireRateTimer = 0.0f;
    private float specialAttackTimer = 0.0f;

    private void Awake()
    {
        fireRate = enemyStats.attackCooldown;
    }


    private void Update()
    {
        //enfriamiento del arma
        if (!canUseWeapon)
        {
            fireRateTimer += Time.deltaTime;

            if (fireRateTimer >= fireRate)
            {
                canUseWeapon = true;
                fireRateTimer = 0.0f;
            }
        }

        if (!canUseSpecialAttack)
        {
            specialAttackTimer += Time.deltaTime;

            if (specialAttackTimer >= specialAttackCooldown)
            {
                canUseSpecialAttack = true;
                specialAttackTimer = 0.0f;
            }
        }
    }

    public void FireWeapon(Vector2 targetDir)
    {
        Vector2 dir = (targetDir - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;

        canUseWeapon = false;
    }

    public void SpecialAttack(Vector2 targetDir)
    {
        Instantiate(specialBulletPrefab, targetDir, Quaternion.identity);
        canUseSpecialAttack = false;
    }
}
