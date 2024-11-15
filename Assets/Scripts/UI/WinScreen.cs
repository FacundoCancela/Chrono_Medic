using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] WinScreen winScreen;
    private PauseManager pauseManager;
    public bool winScreens = false;

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
    }
}
