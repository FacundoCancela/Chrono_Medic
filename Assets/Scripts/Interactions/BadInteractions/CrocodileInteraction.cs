using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileInteraction : MonoBehaviour
{
    [SerializeField] private GameObject crocodileWarning;
    [SerializeField] public Transform player;
    [SerializeField] private bool playerInWater;
    [SerializeField] private float spawnChance;
    private float timeSinceLastCheck = 0f;
    public float checkInterval = 1f;

    void Update()
    {
        if (playerInWater)
        {
            timeSinceLastCheck += Time.deltaTime;
            if (timeSinceLastCheck >= checkInterval)
            {
                timeSinceLastCheck = 0f;
                float randomValue = Random.Range(0f, 100f);
                if (randomValue <= spawnChance)
                {
                    Instantiate(crocodileWarning, player.position, Quaternion.identity);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Transform>();
            playerInWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInWater = false;
        }
    }
}
