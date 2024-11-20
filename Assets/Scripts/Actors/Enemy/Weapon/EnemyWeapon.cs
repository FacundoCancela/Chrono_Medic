using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public EnemyStats enemyStats;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletSpeed = 10.0f;

    public void FireWeapon(Vector2 targetDir, Rigidbody2D targetRigidbody)
    {
        float accuracyOffset = targetRigidbody.velocity == Vector2.zero ? 0 : enemyStats.accuracyOffset;

        Vector2 randomOffset = Random.insideUnitCircle * accuracyOffset;
        Vector2 adjustedDir = (targetDir + randomOffset - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(adjustedDir.y, adjustedDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = adjustedDir * bulletSpeed;

        EnemyAttack enemyAttack = bullet.GetComponent<EnemyAttack>();
        enemyAttack.SetDamage(enemyStats.damage);
        Debug.Log("daño seteado: " + enemyStats.damage);
    }
}
