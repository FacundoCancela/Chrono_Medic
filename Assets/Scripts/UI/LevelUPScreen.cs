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
    [SerializeField] Button unlockMeleeButton;
    [SerializeField] Button unlockRangedButton;
    [SerializeField] Button unlockEngineerButton;
    [SerializeField] Button unlockBoomerangButton;
    [SerializeField] Button unlockCurveSwordButton;
    [SerializeField] ClassManager classManager;

    [SerializeField] TextMeshProUGUI maxWeaponsUnlockedText;

    private List<Button> weaponButtons;

    public int weaponUnlockOptions = 3;
    public int weaponUnlockCount = 0;
    public int maxUnlockeableWeapons = 4;


    private void Start()
    {
        weaponButtons = new List<Button>
        {
            unlockMeleeButton,
            unlockRangedButton,
            unlockEngineerButton,
            unlockBoomerangButton,
            unlockCurveSwordButton,
        };

        switch (ClassManager.currentClass)
        {
            case ClassManager.SelectedClass.Melee:
                weaponButtons.Remove(unlockMeleeButton);
                weaponUnlockCount++;
                unlockMeleeButton.gameObject.SetActive(false);
                break;
            case ClassManager.SelectedClass.Ranged:
                weaponButtons.Remove(unlockRangedButton);
                weaponUnlockCount++;
                unlockRangedButton.gameObject.SetActive(false);
                break;
            case ClassManager.SelectedClass.Engineer:
                weaponButtons.Remove(unlockEngineerButton);
                weaponUnlockCount++;
                unlockEngineerButton.gameObject.SetActive(false);
                break;
        }

        ShuffleAndDisplayButtons();        
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
        DeactivateButton(unlockEngineerButton);
    }

    public void UnlockMelee()
    {
        weaponManager._meleeCanAttack = true;
        DeactivateButton(unlockMeleeButton);
    }

    public void UnlockRanged()
    {
        weaponManager._rangedCanAttack = true;
        DeactivateButton(unlockRangedButton);
    }

    public void UnlockBoomerang()
    {
        weaponManager._boomerangCanAttack = true;
        DeactivateButton(unlockBoomerangButton);
    }

    public void UnlockCurveSword()
    {
        weaponManager._curveSwordCanAttack = true;
        DeactivateButton(unlockCurveSwordButton);
    }

    public void DeactivateButton(Button button)
    {
        ContinueGame();
        gameObject.SetActive(false);
        weaponButtons.Remove(button);
        button.gameObject.SetActive(false);
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
        foreach (var button in weaponButtons)
        {
            button.gameObject.SetActive(false);
        }

        for (int i = 0; i < weaponButtons.Count; i++)
        {
            Button temp = weaponButtons[i];
            int randomIndex = Random.Range(i, weaponButtons.Count);
            weaponButtons[i] = weaponButtons[randomIndex];
            weaponButtons[randomIndex] = temp;
        }
        
        int buttonsToShow = Mathf.Min(weaponUnlockOptions, weaponButtons.Count);

        if (weaponUnlockCount < maxUnlockeableWeapons)
        {
            for (int i = 0; i < buttonsToShow; i++)
            {
                weaponButtons[i].gameObject.SetActive(true);
            }
        }
        else maxWeaponsUnlockedText.text = "Max weapons unlocked";
    }

}
