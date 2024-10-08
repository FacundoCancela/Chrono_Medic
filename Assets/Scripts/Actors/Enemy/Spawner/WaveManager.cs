using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveCount waveCount;
    [SerializeField] WinScreen winScreen;
    [SerializeField] public int maxWave;
    public float waveTimer = 30f;
    [SerializeField] public int actualWave = 0;
    private bool waveInProgress;
    private bool bossBattleInProgress;
    private bool ammitAlreadySpawned;
    private bool anubisAlreadySpawned;

    [SerializeField] public EnemySpawner enemySpawner;
    [SerializeField] public List<GameObject> enemyPrefabs;
    [SerializeField] public List<GameObject> bossPrefabs;
    [SerializeField] public int enemiesInThisWave;
    public int enemiesAlive;

    [SerializeField] public float spawnInterval = 1f;
    [SerializeField] public int maxEnemiesInThisWave = 15;

    private float spawnCooldown = 0f;

    public static WaveManager Instance
    {
        get { return instance; }
    }
    private static WaveManager instance;

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
        ResetTimerAndEnemies();
        StartNextWave();
    }


    private void Update()
    {
        if (waveInProgress)
        {
            spawnCooldown += Time.deltaTime;
            waveTimer -= Time.deltaTime;
            waveCount.UpdateTimer(waveTimer);

            if(waveTimer <= 0f && !bossBattleInProgress)
            {
                ResetTimerAndEnemies();
                StartNextWave();
            }

            if (spawnCooldown >= spawnInterval)
            {
                spawnCooldown = 0f;
                if (enemiesAlive < maxEnemiesInThisWave && !bossBattleInProgress)
                {
                    enemySpawner.SpawnEnemy();
                    enemiesAlive++;
                }
            }
        }
    }

    public void StartNextWave()
    {

        if (actualWave < maxWave)
        {
            actualWave++;
            waveInProgress = true;
        }
        else
        {
            // All waves completed
            waveInProgress = false;
            Win();
        }

        if (actualWave == 5 && !ammitAlreadySpawned)
        {
            enemySpawner.SpawnAmmitBoss();
            bossBattleInProgress = true;
            ammitAlreadySpawned = true;
        }

        if (actualWave == 10 && !anubisAlreadySpawned)
        {
            enemySpawner.SpawnAnubisBoss();
            bossBattleInProgress = true;
            anubisAlreadySpawned = true;
        }


        waveCount.updateWave(actualWave, maxWave);

    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;               
    }

    public void ResetTimerAndEnemies()
    {
        switch(actualWave)
        {
            case 1:
                waveTimer = 20f;
                maxEnemiesInThisWave = 15;
                spawnCooldown = 3;
                break;
            case 2:
                waveTimer = 30f;
                maxEnemiesInThisWave = 20;
                spawnCooldown = 3;
                break;
            case 3:
                waveTimer = 40f;
                maxEnemiesInThisWave = 30;
                spawnCooldown = 3;
                break;
            case 4:
                waveTimer = 50f;
                maxEnemiesInThisWave = 40;
                spawnCooldown = 2.5f;
                break;
            case 5:
                waveTimer = 60f;
                maxEnemiesInThisWave = 50;
                spawnCooldown = 2.5f;
                break;
            case 6:
                waveTimer = 70f;
                maxEnemiesInThisWave = 55;
                spawnCooldown = 2.5f;
                break;
            case 7:
                waveTimer = 80f;
                maxEnemiesInThisWave = 60;
                spawnCooldown = 2;
                break;
            case 8:
                waveTimer = 85f;
                maxEnemiesInThisWave = 65;
                spawnCooldown = 2;
                break;
            case 9:
                waveTimer = 90f;
                maxEnemiesInThisWave = 65;
                spawnCooldown = 2;
                break;
            case 10:
                waveTimer = 180f;
                maxEnemiesInThisWave = 60;
                spawnCooldown = 2;
                break;
            default:
                waveTimer = 30f;
                maxEnemiesInThisWave = 60;
                spawnCooldown = 2;
                break;

        }
    }

    public void EndBossBattle()
    {
        StartNextWave();
        bossBattleInProgress = false;
    }

    public int GetActualWave()
    {
        return actualWave;
    }

    public void Win()
    {
        winScreen.gameObject.SetActive(true);
    }
}
