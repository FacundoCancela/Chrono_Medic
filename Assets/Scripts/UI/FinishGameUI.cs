using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameUI : MonoBehaviour
{
    [SerializeField] public LoseScreen loseScreen;
    [SerializeField] public WinScreen winScreen;

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += LoseScreen;
        WaveManager.OnWin += WinScreen;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= LoseScreen;
        WaveManager.OnWin -= WinScreen;

    }

    private void WinScreen()
    {
        winScreen.gameObject.SetActive(true);
    }

    private void LoseScreen()
    {
        loseScreen.gameObject.SetActive(true);
    }

}
