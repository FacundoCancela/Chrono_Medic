using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileDamage : MonoBehaviour
{
    [SerializeField] private int damage;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.GetDamaged(damage);
            }
        }
    }
}
