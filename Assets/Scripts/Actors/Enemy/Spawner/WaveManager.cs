using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveCount waveCount;
    [SerializeField] public int maxWave;
    [SerializeField] public int actualWave = 0;
    private bool waveInProgress;
    private bool bossBattleInProgress;
    private bool ammitAlreadySpawned;
    private bool anubisAlreadySpawned;

    private DialogueManager dialogueManager;

    public bool EndDialogueBos; // Este bool será true solo después del diálogo de Molo

    [SerializeField] public EnemySpawner enemySpawner;
    public int enemiesAlive;

    [SerializeField] WaveStats waveStats;
    public float waveTimer = 30f;
    public GameObject waveCanvas;

    private float spawnCooldown = 0f;
    [SerializeField] private bool noSpawnEnemies = true;
    [SerializeField] GameObject playerPortal;

    public static event Action OnWin;

    public static WaveManager Instance
    {
        get { return instance; }
    }
    private static WaveManager instance;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        EnemyModel.OnEnemyDeath += HandleEnemyDeath;
        BossModel.OnBossDeath += HandleEnemyDeath;
        BossModel.OnEndBossBattle += EndBossBattle;
    }

    private void OnDisable()
    {
        EnemyModel.OnEnemyDeath -= HandleEnemyDeath;
        BossModel.OnBossDeath -= HandleEnemyDeath;
        BossModel.OnEndBossBattle -= EndBossBattle;
    }

    private void Start()
    {
        ResetTimerAndEnemies();
    }

    private void Update()
    {
        if (EndDialogueBos) // Verifica si es el final del diálogo de Molo
        {
            Win();
            Debug.Log("Win");
        }


        if (dialogueManager != null && dialogueManager.DialogeActive)
        {
            return; // Salir del Update si el juego está pausado
        }

        if (waveInProgress && !noSpawnEnemies)
        {
            spawnCooldown += Time.deltaTime;
            waveTimer -= Time.deltaTime;
            waveCount.UpdateTimer(waveTimer);

            if (waveTimer <= 0f && !bossBattleInProgress)
            {
                ResetTimerAndEnemies();
            }

            

            if (spawnCooldown >= waveStats.spawnInteval[actualWave])
            {
                spawnCooldown = 0f;
                if (enemiesAlive < waveStats.maxEnemiesInThisWave[actualWave] && !bossBattleInProgress)
                {
                    enemySpawner.SpawnEnemy();
                    enemiesAlive++;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            ResetTimerAndEnemies();
        }

    }

    public void StartNextWave()
    {
        if (actualWave < maxWave)
        {
            actualWave++;
            waveInProgress = true;
        }
        StartCoroutine(WaveCanvasDelay(waveCanvas));
        if (actualWave == 5 && !ammitAlreadySpawned)
        {
            enemySpawner.SpawnBoss("Ammit");
            bossBattleInProgress = true;
            ammitAlreadySpawned = true;
        }

        if (actualWave == 10 && !anubisAlreadySpawned)
        {
            playerPortal.SetActive(true);
            bossBattleInProgress = true;
            anubisAlreadySpawned = true;
        }

        waveCount.updateWave(actualWave, maxWave);
    }

    private void HandleEnemyDeath(EnemyDeathData deathData)
    {
        enemiesAlive--;
    }

    public void ResetTimerAndEnemies()
    {
        waveTimer = waveStats.waveTimer[actualWave + 1];
        StartNextWave();
    }

    public void EndBossBattle()
    {
        ResetTimerAndEnemies();
        bossBattleInProgress = false;
    }

    public int GetActualWave()
    {
        return actualWave;
    }

    public void Win()
    {
        OnWin?.Invoke();
    }
    public IEnumerator WaveCanvasDelay(GameObject gameObject)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<TextMeshProUGUI>().text = "Wave:" + actualWave;
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}