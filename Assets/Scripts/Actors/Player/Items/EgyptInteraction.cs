using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EgyptInteraction : MonoBehaviour
{
    private bool canBeActivated = false;
    private bool hasBeenActivated = false;
    private float activationTime = 0f;
    [SerializeField] public float duration;
    [SerializeField] EgyptInteractionSpawnPoint spawnPoint;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (canBeActivated && Input.GetKeyDown(KeyCode.F))
        {
            hasBeenActivated = true;
            Debug.Log("activado");
        }

        if (hasBeenActivated)
        {
            transform.Translate(Vector2.right * Time.deltaTime * 5f);
            activationTime += Time.deltaTime;

            if (activationTime >= duration)
            {
                StopInteraction();
                activationTime = 0f;
            }
        }

    }


    private void StopInteraction()
    {
        hasBeenActivated = false;
        transform.position = initialPosition;
        spawnPoint.GetRespawnWave();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.GetDamaged(9999);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            canBeActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canBeActivated = false;
        }
    }
}
