using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveCount waveCount;
    [SerializeField] WinScreen winScreen;
    [SerializeField] public int maxWave;
    private int actualWave = 1;
    private bool waveInProgress;

    [SerializeField] public EnemySpawner enemySpawner;
    [SerializeField] public List<GameObject> enemyPrefabs;
    [SerializeField] public int enemiesInThisWave;
    private int enemiesToSpawn;
    public int enemiesAlive;

    [SerializeField] public float spawnInterval = 1f;

    private float timer = 0f;

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
        StartNextWave();
    }


    private void Update()
    {
        if (waveInProgress)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                timer = 0f;
                if (enemiesToSpawn > 0)
                {
                    enemySpawner.SpawnEnemy();
                    enemiesToSpawn--;
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
            enemiesToSpawn = enemiesInThisWave;
            enemiesAlive = enemiesToSpawn;
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
        Debug.Log(enemiesAlive);

        if (enemiesAlive <= 0)
        {
            waveInProgress = false;
            StartNextWave();
        }
    }

    public void Win()
    {
        winScreen.gameObject.SetActive(true);
    }



}
