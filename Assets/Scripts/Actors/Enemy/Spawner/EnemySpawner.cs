using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] WaveManager waveManager;
    [SerializeField] List<GameObject> portals;
    [SerializeField] List<SpawnList> spawnList;
    private GameObject activePortal;
    private float portalChangeTimer = 0f;
    [SerializeField] private float portalChangeInterval = 5f; 

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        ChangeActivePortal(); 
    }

    private void Update()
    {
        portalChangeTimer += Time.deltaTime;
        if (portalChangeTimer >= portalChangeInterval)
        {
            ChangeActivePortal(); 
            portalChangeTimer = 0f; 
        }
    }

    public void SpawnEnemy()
    {
        if (playerTransform != null && spawnList[waveManager.actualWave].enemyPrefabs.Count > 0)
        {
            if (activePortal != null)
            {
                int randomIndex = Random.Range(0, spawnList[waveManager.actualWave].enemyPrefabs.Count);
                GameObject selectedEnemyPrefab = spawnList[waveManager.actualWave].enemyPrefabs[randomIndex];

                Instantiate(selectedEnemyPrefab, activePortal.transform.position, Quaternion.identity);
            }
        }
    }

    private void ChangeActivePortal()
    {

        foreach (GameObject portal in portals)
        {
            SpriteRenderer spriteRenderer = portal.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }

        int randomPortalIndex = Random.Range(0, portals.Count);
        activePortal = portals[randomPortalIndex];

        SpriteRenderer activeSpriteRenderer = activePortal.GetComponent<SpriteRenderer>();
        if (activeSpriteRenderer != null)
        {
            activeSpriteRenderer.enabled = true;
        }
    }

    public void SpawnAmmitBoss()
    {
        if (playerTransform != null && activePortal != null)
        {
            GameObject selectedEnemyPrefab = waveManager.bossPrefabs[0];
            Instantiate(selectedEnemyPrefab, activePortal.transform.position, Quaternion.identity);
        }
    }

    public void SpawnAnubisBoss()
    {
        if (playerTransform != null && activePortal != null)
        {
            GameObject selectedEnemyPrefab = waveManager.bossPrefabs[1];
            Instantiate(selectedEnemyPrefab, activePortal.transform.position, Quaternion.identity);
        }
    }
}
