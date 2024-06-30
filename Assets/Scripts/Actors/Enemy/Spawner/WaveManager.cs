using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveCount waveCount;
    [SerializeField] WinScreen winScreen;
    [SerializeField] public int maxWave;
    public float waveTimer = 30f;
    private int actualWave = 1;
    private bool waveInProgress;

    [SerializeField] public EnemySpawner enemySpawner;
    [SerializeField] public List<GameObject> enemyPrefabs;
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

            if(waveTimer <= 0f)
            {
                ResetTimerAndEnemies();
                StartNextWave();
            }

            if (spawnCooldown >= spawnInterval)
            {
                spawnCooldown = 0f;
                if (enemiesAlive < maxEnemiesInThisWave)
                {
                    enemySpawner.SpawnEnemy();
                    enemiesAlive++;
                }
            }
        }
    }

    public void StartNextWave()
    {
        waveCount.updateWave(actualWave, maxWave);

        if (actualWave <= maxWave)
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
                waveTimer = 30f;
                maxEnemiesInThisWave = 15;
                spawnCooldown = 3;
                break;
            case 2:
                waveTimer = 40f;
                maxEnemiesInThisWave = 20;
                spawnCooldown = 3;
                break;
            case 3:
                waveTimer = 50f;
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

    public int GetActualWave()
    {
        return actualWave;
    }

    public void Win()
    {
        winScreen.gameObject.SetActive(true);
    }
}
