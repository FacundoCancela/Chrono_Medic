using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public float minSpawnRange = 5f;
    public float maxSpawnRange = 10f;
    private Transform playerTransform;
    private float timer = 0f;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform; // Encuentra el jugador y obt�n su transform
    }

    private void Update()
    {
        timer += Time.deltaTime; // Incrementa el temporizador en cada frame

        // Si el temporizador ha alcanzado el intervalo de aparici�n
        if (timer >= spawnInterval)
        {
            timer = 0f; // Reinicia el temporizador
            SpawnEnemy(); // Invoca la funci�n para spawnear un enemigo
        }
    }

    void SpawnEnemy()
    {
        if (playerTransform != null)
        {
            // Genera una posici�n aleatoria dentro del rango alrededor del jugador
            Vector2 spawnOffset = Random.insideUnitCircle.normalized * Random.Range(minSpawnRange, maxSpawnRange);
            // Aplica la posici�n relativa al jugador
            Vector2 spawnPosition = (Vector2)playerTransform.position + spawnOffset;

            // Instancia el enemigo en la posici�n generada
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
