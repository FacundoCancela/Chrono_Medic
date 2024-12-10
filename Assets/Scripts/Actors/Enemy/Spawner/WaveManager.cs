using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveCount waveCount;
    [SerializeField] WinScreen winScreen;
    [SerializeField] public int maxWave;
    [SerializeField] public int actualWave = 0;
    private bool waveInProgress;
    private bool bossBattleInProgress;
    private bool ammitAlreadySpawned;
    private bool anubisAlreadySpawned;

    private DialogueManager dialogueManager;

    public bool EndDialogueBos; // Este bool será true solo después del diálogo de Molo

    [SerializeField] public EnemySpawner enemySpawner;
    [SerializeField] public List<GameObject> bossPrefabs;
    public int enemiesAlive;

    [SerializeField] WaveStats waveStats;
    public float waveTimer = 30f;
    public GameObject waveCanvas;

    private float spawnCooldown = 0f;
    [SerializeField] private bool noSpawnEnemies = true;
    [SerializeField] GameObject playerPortal;

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
    }

    private void OnDisable()
    {
        EnemyModel.OnEnemyDeath -= HandleEnemyDeath;
        BossModel.OnBossDeath -= HandleEnemyDeath;
    }

    private void Start()
    {
        ResetTimerAndEnemies();
        StartNextWave();
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
                StartNextWave();
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
            StartNextWave();
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
            enemySpawner.SpawnAmmitBoss();
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
    public IEnumerator WaveCanvasDelay(GameObject gameObject)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<TextMeshProUGUI>().text = "Wave:" + actualWave;
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}