using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] WaveManager waveManager;
    [SerializeField] List<GameObject> portals;
    [SerializeField] List<GameObject> bossPortals;
    [SerializeField] List<SpawnList> spawnList;
    private GameObject activePortal;
    private float portalChangeTimer = 0f;
    [SerializeField] private float portalChangeInterval = 5f;


    public static EnemySpawner Instance
    {
        get { return instance; }
    }
    private static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    public void SpawnAmmitAndAnubisBoss()
    {
        if (playerTransform != null && activePortal != null)
        {
            ActivateBossPortal();
            GameObject selectedEnemyPrefab = waveManager.bossPrefabs[1];
            Instantiate(selectedEnemyPrefab, bossPortals[0].transform.position, Quaternion.identity);
        }
    }

    public void ActivateBossPortal()
    {
        SpriteRenderer spriteRenderer = bossPortals[0].GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

    }


    public void SpawnAnubisBoss(Vector3 spawnPosition)
    {
        if (playerTransform != null && activePortal != null)
        {
            GameObject selectedEnemyPrefab = waveManager.bossPrefabs[2];
            Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
