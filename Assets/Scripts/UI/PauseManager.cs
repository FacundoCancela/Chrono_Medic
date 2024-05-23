using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    public bool gamePaused;

    private void Update()
    {
        PauseKey();
    }

    public void PauseKey()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            PauseAndContinue();
        }
    }

    public void PauseAndContinue()
    {
        if(!gamePaused)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            gamePaused = true;
        }
        else if(gamePaused)
        {
            Time.timeScale = 1.0f;
            pauseScreen.SetActive(false);
            gamePaused = false;
        }
    }
}
