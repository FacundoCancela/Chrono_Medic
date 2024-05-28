using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] PlayerController playerController;
    public bool gamePaused;
    public bool canPause = true;

    private void Update()
    {
        PauseKey();
    }

    public void PauseKey()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && canPause) 
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

    public void Continue()
    {
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
        gamePaused = false;
    }

    public void Surrender()
    {
        playerController.Die();
        PauseAndContinue();
    }

}
