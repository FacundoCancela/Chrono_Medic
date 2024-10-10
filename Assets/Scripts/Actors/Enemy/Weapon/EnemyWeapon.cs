using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public EnemyStats enemyStats;
    public bool CanUseWeapon => canUseWeapon;

    private bool canUseWeapon = true;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletSpeed = 10.0f;
    [SerializeField] private float fireRate;

    [SerializeField] private float accuracyOffset = 3f;


    private float fireRateTimer = 0.0f;

    private void Awake()
    {
        fireRate = enemyStats.attackCooldown;
    }


    private void Update()
    {
        //enfriamiento del arma
        if(!canUseWeapon)
        {
            fireRateTimer += Time.deltaTime;

            if(fireRateTimer >= fireRate)
            {
                canUseWeapon = true;
                fireRateTimer = 0.0f;
            }
        }
    }

    public void FireWeapon(Vector2 targetDir)
    {
        Vector2 randomOffset = Random.insideUnitCircle * accuracyOffset;
        Vector2 adjustedDir = (targetDir + randomOffset - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(adjustedDir.y, adjustedDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = adjustedDir * bulletSpeed;

        canUseWeapon = false;
    }
}
