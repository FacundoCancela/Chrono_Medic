using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] WaveManager waveManager;
    [SerializeField] List<GameObject> portals;
    [SerializeField] List<SpawnList> spawnList;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform; 
    }

    public void SpawnEnemy()
    {
        if (playerTransform != null && spawnList[waveManager.actualWave].enemyPrefabs.Count > 0)
        {
            GameObject nearestPortal = GetNearestPortal();

            if (nearestPortal != null)
            {
                foreach (GameObject portal in portals)
                {
                    SpriteRenderer spriteRenderer = portal.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.enabled = portal == nearestPortal;
                    }
                }

                int randomIndex = Random.Range(0, spawnList[waveManager.actualWave].enemyPrefabs.Count);
                GameObject selectedEnemyPrefab = spawnList[waveManager.actualWave].enemyPrefabs[randomIndex];

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

    public void SpawnAnubisBoss()
    {
        if (playerTransform != null)
        {
            GameObject nearestPortal = GetNearestPortal();

            if (nearestPortal != null)
            {
                GameObject selectedEnemyPrefab = waveManager.bossPrefabs[1];
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
