using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUPScreen : MonoBehaviour
{
    [SerializeField] ExperienceManager experienceManager;
    [SerializeField] PauseManager pauseManager;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] Button unlockMeleeButton;
    [SerializeField] Button unlockRangedButton;
    [SerializeField] Button unlockEnineerButton;
    [SerializeField] ClassManager classManager;

    private void Start()
    {
        switch (ClassManager.currentClass)
        {
            case ClassManager.SelectedClass.Melee:
                unlockMeleeButton.gameObject.SetActive(false);
                break;
            case ClassManager.SelectedClass.Ranged:
                unlockRangedButton.gameObject.SetActive(false);
                break;
            case ClassManager.SelectedClass.Engineer:
                unlockEnineerButton.gameObject.SetActive(false);
                break;
        }
    }

    //Weapon upgrade

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

    //Weapon unlock


    public void UnlockOrbital()
    {
        weaponManager._engineerCanAttack = true;
        ContinueGame();
        gameObject.SetActive(false);
        unlockEnineerButton.gameObject.SetActive(false);
    }

    public void UnlockMelee()
    {
        weaponManager._meleeCanAttack = true;
        ContinueGame();
        gameObject.SetActive(false);
        unlockMeleeButton.gameObject.SetActive(false);
    }

    public void UnlockRanged()
    {
        weaponManager._rangedCanAttack = true;
        ContinueGame();
        gameObject.SetActive(false);
        unlockRangedButton.gameObject.SetActive(false);
    }

    public void ContinueGame()
    {
        pauseManager.canPause = true;
        Time.timeScale = 1.0f;
    }
}
