using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] LoseScreen loseScreen;
    private PauseManager pauseManager;

    private void Start()
    {
        // Buscar una instancia de PauseManager en la escena y asignarla a pauseManager
        pauseManager = FindObjectOfType<PauseManager>();

        // Verificar si se encontró el PauseManager
        if (pauseManager != null)
        {
            Time.timeScale = 0.0f;
            pauseManager.canPause = false;
        }
        else
        {
            Debug.LogError("No se encontró un PauseManager en la escena.");
        }
    }
}
