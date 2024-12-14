using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] WaveManager waveManager;
    [SerializeField] List<GameObject> portals;
    [SerializeField] List<GameObject> bossPortals;
    [SerializeField] List<SpawnList> spawnList;
    [SerializeField] List<BossData> bossDataList;
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

    public void SpawnBoss(string bossName, Vector3? spawnPosition = null)
    {
        if(bossName == "AmmitAndAnubis")
            ActivateBossPortal();

        if (playerTransform != null)
        {
            BossData bossData = bossDataList.Find(boss => boss.bossName == bossName);

            if (bossData != null)
            {
                Vector3 position = spawnPosition ?? activePortal.transform.position;
                Instantiate(bossData.bossPrefab, position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"No se encontró un jefe con el nombre {bossName}.");
            }
        }
    }

    public void ActivateBossPortal()
    {
        SpriteRenderer spriteRenderer = bossPortals[0].GetComponent<SpriteRenderer>();
        activePortal = bossPortals[0];
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

    }
}

[System.Serializable]
public class BossData
{
    public string bossName;
    public GameObject bossPrefab;
}