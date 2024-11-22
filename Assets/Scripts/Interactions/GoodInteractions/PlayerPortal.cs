using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = destination.position;
            EnemySpawner.Instance.SpawnAmmitAndAnubisBoss();
            arrow.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (arrow != null && player != null)
        {
            arrow.SetActive(true);
        }
    }

    private void Update()
    {
        if (arrow.activeSelf)
        {
            Vector2 direction = (transform.position - player.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

}
