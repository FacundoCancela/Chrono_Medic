using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] LoseScreen loseScreen;
    private PauseManager pauseManager;

    private void Start()
    {
       
        pauseManager = FindObjectOfType<PauseManager>();

       
        if (pauseManager != null)
        {
            Time.timeScale = 0.0f;
            pauseManager.canPause = false;
        }
    }
}
