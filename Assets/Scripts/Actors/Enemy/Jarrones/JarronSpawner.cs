using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarronSpawner : MonoBehaviour
{
    public Jarron jarronPrefab;
    public float spawnInterval = 2.0f; 
    private BoxCollider2D spawnArea; 

    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        if (spawnArea != null)
        {
            StartCoroutine(SpawnJarrones());
        }
    }

    IEnumerator SpawnJarrones()
    {
        while (true)
        {
            SpawnJarron();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnJarron()
    {
        if (jarronPrefab != null && spawnArea != null)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
            );
            Jarron jarron = Instantiate(jarronPrefab, randomPosition, Quaternion.identity, transform);
        }
    }
}
