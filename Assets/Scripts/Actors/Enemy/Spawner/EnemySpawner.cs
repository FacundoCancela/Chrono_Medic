using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] WaveManager waveManager;
    [SerializeField] List<GameObject> portals; // Lista de portales

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform; // Encuentra el jugador y obtén su transform
    }

    public void SpawnEnemy()
    {
        if (playerTransform != null && waveManager.enemyPrefabs.Count > 0)
        {
            // Encuentra el portal más cercano al jugador
            GameObject nearestPortal = GetNearestPortal();

            if (nearestPortal != null)
            {
                // Desactiva los sprites de todos los portales excepto el más cercano
                foreach (GameObject portal in portals)
                {
                    SpriteRenderer spriteRenderer = portal.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.enabled = portal == nearestPortal;
                    }
                }

                // Selecciona un índice aleatorio de la lista de prefabs de enemigos
                int randomIndex = Random.Range(0, waveManager.enemyPrefabs.Count);
                GameObject selectedEnemyPrefab = waveManager.enemyPrefabs[randomIndex];

                // Instancia el enemigo en la posición del portal más cercano
                Instantiate(selectedEnemyPrefab, nearestPortal.transform.position, Quaternion.identity);
            }
        }
    }

    public void SpawnAmmitBoss()
    {
        if (playerTransform != null)
        {
            GameObject nearestPortal = GetNearestPortal();

            if (nearestPortal != null)
            {
                GameObject selectedEnemyPrefab = waveManager.bossPrefabs[0];
                Instantiate(selectedEnemyPrefab, nearestPortal.transform.position, Quaternion.identity);
            }
        }
    }


    private GameObject GetNearestPortal()
    {
        GameObject nearestPortal = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject portal in portals)
        {
            float distanceToPortal = Vector2.Distance(playerTransform.position, portal.transform.position);
            if (distanceToPortal < closestDistance)
            {
                closestDistance = distanceToPortal;
                nearestPortal = portal;
            }
        }

        return nearestPortal;
    }
}
