using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public bool CanUseWeapon => canUseWeapon;

    private bool canUseWeapon = true;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 10.0f;
    [SerializeField] private float fireRate = 1.0f;
    [SerializeField] private Transform shootPoint;

    private float fireRateTimer = 0.0f;

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
        GameObject newBullet = Instantiate(bullet,shootPoint);
        Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
        newBulletRb.AddForce((targetDir).normalized * bulletSpeed);
        canUseWeapon = false;
    }
}
