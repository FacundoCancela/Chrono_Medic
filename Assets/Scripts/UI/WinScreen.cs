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
        
        pauseManager = FindObjectOfType<PauseManager>();

        
        if (pauseManager != null)
        {
            pauseManager.PauseWithoutCanvas();
            pauseManager.canPause = false;
        }
    }
}
