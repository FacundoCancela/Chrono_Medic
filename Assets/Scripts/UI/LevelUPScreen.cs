using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUPScreen : MonoBehaviour
{
    [SerializeField] ExperienceManager experienceManager;
    [SerializeField] PauseManager pauseManager;

    public void MeleeDamage()
    {
        experienceManager.MeleeDamage();
        ContinueGame();
        gameObject.SetActive(false);
    }

    public void RangedDamage()
    {
        experienceManager.RangedDamage();
        ContinueGame();
        gameObject.SetActive(false);
    }

    public void OrbitalDamage()
    {
        experienceManager.OrbitalDamage();
        ContinueGame();
        gameObject.SetActive(false);
    }

    public void ContinueGame()
    {
        pauseManager.canPause = true;
        Time.timeScale = 1.0f;
    }
}
