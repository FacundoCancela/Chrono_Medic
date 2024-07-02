using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] public string nombreEscenaAJugar;
    private PauseManager pauseManager;

    GameManager gameManager;

    private void Awake()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void CambiarEscenaDefault()
    {
        // Cambiar a la escena especificada
        if(gameManager.exitMainMenu != null)
            gameManager.exitMainMenu.SetActive(true);

        SceneManager.LoadScene(nombreEscenaAJugar);
        if (pauseManager != null) 
            pauseManager.canPause = true;
    }

    public void CambiarEscena(string nombreEscenaAJugar)
    {
        // Cambiar a la escena especificada
        SceneManager.LoadScene(nombreEscenaAJugar);
        if (pauseManager != null)
            pauseManager.canPause = true;
    }

}
