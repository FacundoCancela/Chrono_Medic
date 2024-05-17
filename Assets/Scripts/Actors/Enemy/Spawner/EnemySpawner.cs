using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float minSpawnRange = 10f;
    public float maxSpawnRange = 15f;
    private Transform playerTransform;
    [SerializeField] WaveManager waveManager;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform; // Encuentra el jugador y obt�n su transform
    }

    public void SpawnEnemy()
    {
        if (playerTransform != null && waveManager.enemyPrefabs.Count > 0)
        {
            // Selecciona un �ndice aleatorio de la lista de prefabs de enemigos
            int randomIndex = Random.Range(0, waveManager.enemyPrefabs.Count);
            GameObject selectedEnemyPrefab = waveManager.enemyPrefabs[randomIndex];

            // Genera una posici�n aleatoria dentro del rango alrededor del jugador
            Vector2 spawnOffset = Random.insideUnitCircle.normalized * Random.Range(minSpawnRange, maxSpawnRange);
            // Aplica la posici�n relativa al jugador
            Vector2 spawnPosition = (Vector2)playerTransform.position + spawnOffset;

            // Instancia el enemigo en la posici�n generada
            Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
