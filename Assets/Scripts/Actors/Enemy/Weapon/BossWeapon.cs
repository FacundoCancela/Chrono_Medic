using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public enum BossType { Ammit, AmmitAndAnubis, Anubis,  }
    public BossType bossType;

    public EnemyStats enemyStats;
    public bool CanUseWeapon => canUseWeapon;
    public bool CanUseSpecialAttack => canUseSpecialAttack;

    public bool canUseWeapon = true;
    public bool canUseSpecialAttack = false;

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

    public void Attack(Vector2 targetDir, Rigidbody2D targetRigidbody)
    {
        if (CanUseSpecialAttack)
        {
            if(bossType == BossType.Ammit)
                AmmitSpecialAttack(targetDir);
            else
                SpecialAttack(targetDir);
        }
        else if (CanUseWeapon)
        {
            FireWeapon(targetDir, targetRigidbody);
        }
    }

    public void FireWeapon(Vector2 targetDir, Rigidbody2D targetRigidbody)
    {
        float accuracyOffset = targetRigidbody.velocity == Vector2.zero ? 0 : enemyStats.accuracyOffset;

        Vector2 randomOffset = Random.insideUnitCircle * accuracyOffset;
        Vector2 adjustedDir = (targetDir + randomOffset - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(adjustedDir.y, adjustedDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = adjustedDir * bulletSpeed;

        canUseWeapon = false;
    }


    public void SpecialAttack(Vector2 targetDir)
    {
        Instantiate(specialBulletPrefab, targetDir, Quaternion.identity);
        canUseSpecialAttack = false;
        canUseWeapon = false;
    }

    public void AmmitSpecialAttack(Vector2 targetDir)
    {
        //Ejecutar animacion ademas de esto
        Instantiate(specialBulletPrefab, targetDir, Quaternion.identity);
        canUseSpecialAttack = false;
        canUseWeapon = false;
    }

}
