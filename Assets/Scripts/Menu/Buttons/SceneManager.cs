using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] public string nombreEscenaAJugar;
    private PauseManager pauseManager;

    private void Awake()
    {
        pauseManager = FindObjectOfType<PauseManager>();
    }

    public void CambiarEscenaDefault()
    {
        // Cambiar a la escena especificada
        UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscenaAJugar);
        if (pauseManager != null) 
            pauseManager.canPause = true;
    }

    public void CambiarEscena(string nombreEscenaAJugar)
    {
        // Cambiar a la escena especificada
        UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscenaAJugar);
        if (pauseManager != null)
            pauseManager.canPause = true;
    }
}
