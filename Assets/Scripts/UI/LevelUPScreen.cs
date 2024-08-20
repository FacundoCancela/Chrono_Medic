using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUPScreen : MonoBehaviour
{
    [SerializeField] ExperienceManager experienceManager;
    [SerializeField] PauseManager pauseManager;
    [SerializeField] WeaponManager weaponManager;

    [SerializeField] ClassManager classManager;

    //Unlock Weapon Buttons
    [SerializeField] Button unlockMeleeButton;
    [SerializeField] Button unlockRangedButton;
    [SerializeField] Button unlockEngineerButton;
    [SerializeField] Button unlockBoomerangButton;
    [SerializeField] Button unlockCurveSwordButton;

    //Upgrade Weapon Buttons
    [SerializeField] Button upgradeMeleeButton;
    [SerializeField] Button upgradeRangedButton;
    [SerializeField] Button upgradeEngineerButton;
    [SerializeField] Button upgradeBoomerangButton;
    [SerializeField] Button upgradeCurveSwordButton;

    private List<Button> unlockButtons;
    private List<Button> upgradeButtons;
    private List<Button> availableButtons;

    public int weaponUnlockOptions = 3;
    public int weaponUnlockCount = 0;
    public int maxUnlockeableWeapons = 4;


    private void Start()
    {
        unlockButtons = new List<Button>
        {
            unlockMeleeButton,
            unlockRangedButton,
            unlockEngineerButton,
            unlockBoomerangButton,
            unlockCurveSwordButton,
        };

        upgradeButtons = new List<Button>
        {
           upgradeMeleeButton,
           upgradeRangedButton,
           upgradeEngineerButton,
           upgradeBoomerangButton,
           upgradeCurveSwordButton,
        };

        foreach (var button in upgradeButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (var button in unlockButtons)
        {
            button.gameObject.SetActive(false);
        }

        switch (ClassManager.currentClass)
        {
            case ClassManager.SelectedClass.Melee:
                unlockButtons.Remove(unlockMeleeButton);
                weaponUnlockCount++;
                unlockMeleeButton.gameObject.SetActive(false);
                break;
            case ClassManager.SelectedClass.Ranged:
                unlockButtons.Remove(unlockRangedButton);
                weaponUnlockCount++;
                unlockRangedButton.gameObject.SetActive(false);
                break;
            case ClassManager.SelectedClass.Engineer:
                unlockButtons.Remove(unlockEngineerButton);
                weaponUnlockCount++;
                unlockEngineerButton.gameObject.SetActive(false);
                break;
        }

        availableButtons = new List<Button>(unlockButtons);

        ShuffleAndDisplayButtons();        
    }

    //Weapon upgrade

    public void MeleeLevel()
    {
        experienceManager.MeleeLevelUp();
        ContinueGame();
        gameObject.SetActive(false);
        
    }

    public void RangedLevel()
    {
        experienceManager.RangedLevelUp();
        ContinueGame();
        gameObject.SetActive(false);
    }

    public void EngineerLevel()
    {
        experienceManager.EngineerLevelUp();
        ContinueGame();
        gameObject.SetActive(false);
        
    }

    public void BoomerangLevel()
    {
        experienceManager.BoomerangLevelUp();
        ContinueGame();
        gameObject.SetActive(false);

    }

    public void CurveSwordLevel()
    {
        experienceManager.CurveSwordLevelUp();
        ContinueGame();
        gameObject.SetActive(false);

    }

    //Weapon unlock


    public void UnlockOrbital()
    {
        experienceManager.EngineerLevelUp();
        weaponManager._engineerCanAttack = true;
        IWeapon orbe = FindAnyObjectByType<OrbeAttack>();
        weaponManager.AddManualWeapon(orbe);
        AlternateButton(unlockEngineerButton, upgradeEngineerButton);
    }

    public void UnlockMelee()
    {
        experienceManager.MeleeLevelUp();
        weaponManager._meleeCanAttack = true;
        IWeapon sword = FindAnyObjectByType<SwordAttack>();
        weaponManager.AddManualWeapon(sword);
        AlternateButton(unlockMeleeButton, upgradeMeleeButton);
    }
    public void UnlockCurveSword()
    {
        experienceManager.CurveSwordLevelUp();
        weaponManager._curveSwordCanAttack = true;
        IWeapon curveSword = FindAnyObjectByType<CurveAttack>();
        weaponManager.AddManualWeapon(curveSword);
        AlternateButton(unlockCurveSwordButton, upgradeCurveSwordButton);
    }

    public void UnlockRanged()
    {
        experienceManager.RangedLevelUp();
        weaponManager._rangedCanAttack = true;
        IWeapon ranged = FindAnyObjectByType<RangedAttack>();
        weaponManager.AddAutomaticWeapon(ranged);
        AlternateButton(unlockRangedButton, upgradeRangedButton);
    }

    public void UnlockBoomerang()
    {
        experienceManager.BoomerangLevelUp();
        weaponManager._boomerangCanAttack = true;
        IWeapon boomerang = FindAnyObjectByType<BoomerangAttack>();
        weaponManager.AddAutomaticWeapon(boomerang);
        AlternateButton(unlockBoomerangButton, upgradeBoomerangButton);
    }


    public void AlternateButton(Button unlockButton, Button upgradeButton)
    {
        ContinueGame();
        gameObject.SetActive(false);
        availableButtons.Remove(unlockButton);
        availableButtons.Add(upgradeButton);
        unlockButton.gameObject.SetActive(false);
        weaponUnlockCount++;
        ShuffleAndDisplayButtons();
    }


    //Extra features
    public void ContinueGame()
    {
        pauseManager.canPause = true;
        Time.timeScale = 1.0f;
    }

    private void ShuffleAndDisplayButtons()
    {
        foreach (var button in availableButtons)
        {
            button.gameObject.SetActive(false);
        }

        if (weaponUnlockCount >= maxUnlockeableWeapons)
        {
            availableButtons.RemoveAll(button => unlockButtons.Contains(button));
        }

        for (int i = 0; i < availableButtons.Count; i++)
        {
            Button temp = availableButtons[i];
            int randomIndex = Random.Range(i, availableButtons.Count);
            availableButtons[i] = availableButtons[randomIndex];
            availableButtons[randomIndex] = temp;
        }

        int buttonsToShow = Mathf.Min(weaponUnlockOptions, availableButtons.Count);

        for (int i = 0; i < buttonsToShow; i++)
        {
            availableButtons[i].gameObject.SetActive(true);
        }
    }

}
