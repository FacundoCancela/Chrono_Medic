using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    //Extra buttons
    [SerializeField] Button moneyButton;
    [SerializeField] List<GameObject> numbersObjects;

    private List<Button> unlockButtons;
    private List<Button> upgradeButtons;
    private List<Button> extraButtons;
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

        extraButtons = new List<Button>
        {
            moneyButton,
        };

        foreach (var button in upgradeButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (var button in unlockButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (var button in extraButtons)
        {
            button.gameObject.SetActive(false);
        }


        availableButtons = new List<Button>(unlockButtons);


        switch (ClassManager.currentClass)
        {
            case ClassManager.SelectedClass.Melee:
                availableButtons.Remove(unlockMeleeButton);
                availableButtons.Add(upgradeMeleeButton);
                weaponUnlockCount++;
                unlockMeleeButton.gameObject.SetActive(false);
                break;
            case ClassManager.SelectedClass.Ranged:
                availableButtons.Remove(unlockRangedButton);
                availableButtons.Add(upgradeRangedButton);
                weaponUnlockCount++;
                unlockRangedButton.gameObject.SetActive(false);
                break;
            case ClassManager.SelectedClass.Engineer:
                availableButtons.Remove(unlockEngineerButton);
                availableButtons.Add(upgradeEngineerButton);
                weaponUnlockCount++;
                unlockEngineerButton.gameObject.SetActive(false);
                break;
        }

        ShuffleAndDisplayButtons();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && availableButtons.Count > 0)
        {
            availableButtons[0].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && availableButtons.Count > 1)
        {
            availableButtons[1].onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && availableButtons.Count > 2)
        {
            availableButtons[2].onClick.Invoke();
        }
    }

    //Weapon upgrade

    public void MeleeLevel()
    {
        experienceManager.MeleeLevelUp();
        if (experienceManager.actualMeleeLevel >= experienceManager.maxUpgradeableLevel)
        {
            availableButtons.Remove(upgradeMeleeButton);
            upgradeMeleeButton.gameObject.SetActive(false);
        }
        ContinueGame();
    }

    public void RangedLevel()
    {
        experienceManager.RangedLevelUp();
        if (experienceManager.actualRangedLevel >= experienceManager.maxUpgradeableLevel)
        {
            availableButtons.Remove(upgradeRangedButton);
            upgradeRangedButton.gameObject.SetActive(false);
        }
        ContinueGame();
    }

    public void EngineerLevel()
    {
        experienceManager.EngineerLevelUp();
        if (experienceManager.actualEngineerLevel >= experienceManager.maxUpgradeableLevel)
        {
            availableButtons.Remove(upgradeEngineerButton);
            upgradeEngineerButton.gameObject.SetActive(false);
        }
        ContinueGame();
    }

    public void BoomerangLevel()
    {
        experienceManager.BoomerangLevelUp();
        if (experienceManager.actualBoomerangLevel >= experienceManager.maxUpgradeableLevel)
        {
            availableButtons.Remove(upgradeBoomerangButton);
            upgradeBoomerangButton.gameObject.SetActive(false);
        }
        ContinueGame();
    }

    public void CurveSwordLevel()
    {
        experienceManager.CurveSwordLevelUp();
        if (experienceManager.actualCurveSwordLevel >= experienceManager.maxUpgradeableLevel)
        {
            availableButtons.Remove(upgradeCurveSwordButton);
            upgradeCurveSwordButton.gameObject.SetActive(false);
        }
        ContinueGame();
    }

    //Weapon unlock


    public void UnlockOrbital()
    {
        experienceManager.EngineerLevelUp();
        weaponManager._engineerCanAttack = true;
        IWeapon orbe = FindAnyObjectByType<OrbeAttack>();
        weaponManager.AddAutomaticWeapon(orbe);
        AlternateButton(unlockEngineerButton, upgradeEngineerButton);
    }

    public void UnlockMelee()
    {
        experienceManager.MeleeLevelUp();
        weaponManager._meleeCanAttack = true;
        IWeapon sword = FindAnyObjectByType<SwordAttack>();
        weaponManager.AddAutomaticWeapon(sword);
        AlternateButton(unlockMeleeButton, upgradeMeleeButton);
    }
    public void UnlockCurveSword()
    {
        experienceManager.CurveSwordLevelUp();
        weaponManager._curveSwordCanAttack = true;
        IWeapon curveSword = FindAnyObjectByType<CurveAttack>();
        weaponManager.AddAutomaticWeapon(curveSword);
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
        availableButtons.Remove(unlockButton);
        availableButtons.Add(upgradeButton);
        unlockButton.gameObject.SetActive(false);
        weaponUnlockCount++;
        ContinueGame();
    }

    //Last Level

    public void Money()
    {
        GameDataController.Instance.IncreaseMoney(100);
        ContinueGame();
    }


    //Extra features
    public void ContinueGame()
    {
        ShuffleAndDisplayButtons();
        pauseManager.canPause = true;
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
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

        if (availableButtons.Count == 0)
        {
            availableButtons.AddRange(extraButtons);
        }

        Button classButton = null;
        switch (ClassManager.currentClass)
        {
            case ClassManager.SelectedClass.Melee:
                classButton = upgradeMeleeButton;
                break;
            case ClassManager.SelectedClass.Ranged:
                classButton = upgradeRangedButton;
                break;
            case ClassManager.SelectedClass.Engineer:
                classButton = upgradeEngineerButton;
                break;
        }

        if (classButton != null && availableButtons.Contains(classButton))
        {
            availableButtons.Remove(classButton);
            availableButtons.Insert(0, classButton);
        }

        for (int i = 1; i < availableButtons.Count; i++)
        {
            int randomIndex = Random.Range(i, availableButtons.Count);
            Button temp = availableButtons[i];
            availableButtons[i] = availableButtons[randomIndex];
            availableButtons[randomIndex] = temp;
        }

        int buttonsToShow = Mathf.Min(weaponUnlockOptions, availableButtons.Count);

        for (int i = buttonsToShow - 1; i >= 0; i--)
        {
            availableButtons[i].gameObject.SetActive(true);
            availableButtons[i].transform.SetSiblingIndex(i);
        }

        int buttonsToActivate = Mathf.Min(weaponUnlockOptions, availableButtons.Count);

        for (int i = buttonsToActivate; i < numbersObjects.Count; i++)
        {
            numbersObjects[i].SetActive(false);
        }

    }

}
