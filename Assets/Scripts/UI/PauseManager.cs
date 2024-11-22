using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] PlayerController playerController;
    [SerializeField] SceneChanger sceneChanger;
    [SerializeField] AudioSource levelMusic;
    public bool gamePaused;
    public bool canPause = true;

    private void Update()
    {
        PauseKey();

        if(!gamePaused)
        {
            if (canPause) playerController.playerControllable = true;
            else playerController.playerControllable = false;
        }

        if (gamePaused && levelMusic != null)
        {
            levelMusic.mute = true;
        }
        else if (!gamePaused && levelMusic != null)
        {
            levelMusic.mute = false;
        }

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
            playerController.playerControllable = false;
        }
        else if(gamePaused)
        {
            Time.timeScale = 1.0f;
            pauseScreen.SetActive(false);
            gamePaused = false;
            playerController.playerControllable = true;

        }
        playerController.UpdateCursorState();
    }

    public void PauseWithoutCanvas()
    {
        Time.timeScale = 0f;
        gamePaused = true;
        playerController.playerControllable = false;
        playerController.UpdateCursorState();
    }

    public void Continue()
    {
        playerController.playerControllable = true;
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
        gamePaused = false;

    }

    public void Surrender()
    {
        PauseAndContinue();
        sceneChanger.CambiarEscena("Lvl_Menu");
    }

}
