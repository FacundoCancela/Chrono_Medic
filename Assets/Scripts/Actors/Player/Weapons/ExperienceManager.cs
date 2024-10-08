using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] LevelUPScreen levelUPScreen;
    [SerializeField] PauseManager pausedManager;
    [SerializeField] XPBar XpBar;

    public int actualExperience;
    public int maxExperience;
    public int extraExperiencePerLevel;
    public int level;
    public int maxUpgradeableLevel = 8;

    [Header("Melee")]
    public int actualMeleeLevel = 0;
    public int extraMeleeDamage = 1;
    public float _meleeCooldown = 1f;
    [Header("Ranged")]
    public int actualRangedLevel = 0;
    public int extraRangedDamage = 1;
    public float _rangedCooldown = 1f;
    [Header("Engineer")]
    public int actualEngineerLevel = 0;
    public int extraOrbitalDamage = 1;
    public float _orbitalCooldown = 1f;
    public float _orbitalDuration = 5f;
    public int orbitalSpeed = 100;
    public int orbitalRange = 1;
    public int numberOfOrbs = 1;
    [Header("Curve")]
    public int actualCurveSwordLevel = 0;
    public int extraCurveSwordDamage = 1;
    public float _curveSwordCooldown = 4f;
    public float _curveSwordDuration = 4f;
    public int curveSwordSpeed = 1000;
    [Header("Boomerang")]
    public int actualBoomerangLevel = 0;
    public int extraBoomerangDamage = 1;
    public float _boomerangCooldown = 1f;

    private void Awake()
    {
        XpBar.SetMaxXP(maxExperience);
    }

    private void Start()
    {
        ResetTemporalStats();
    }

    public void gainExperience(int experienceGained)
    {
        actualExperience += experienceGained;
        if (actualExperience >= maxExperience) LevelUp();
        XpBar.SetXP(actualExperience);
    }


    public void LevelUp()
    {
        levelUPScreen.gameObject.SetActive(true);

        pausedManager.canPause = false;
        Time.timeScale = 0f;
        
        actualExperience = 0;
        maxExperience += extraExperiencePerLevel;
        level++;

        XpBar.SetMaxXP(maxExperience);
    }

    //Level up efects

    public void MeleeLevelUp()
    {
        actualMeleeLevel++;
        switch (actualMeleeLevel)
        {
            case 1:
                extraMeleeDamage = 10;
                _meleeCooldown = 2f;
                break;
            case 2:
                extraMeleeDamage = 15;
                _meleeCooldown = 2f;
                break;
            case 3:
                extraMeleeDamage = 15;
                _meleeCooldown = 1.5f;
                break;
            case 4:
                extraMeleeDamage = 25;
                _meleeCooldown = 1.5f;
                break;
            case 5:
                extraMeleeDamage = 25;
                _meleeCooldown = 1f;
                break;
            case 6:
                extraMeleeDamage = 30;
                _meleeCooldown = 1f;
                break;
            case 7:
                extraMeleeDamage = 30;
                _meleeCooldown = 0.75f;
                break;
            case 8:
                extraMeleeDamage = 35;
                _meleeCooldown = 0.5f;
                break;
        }
    }

    public void RangedLevelUp()
    {
        actualRangedLevel++;
        switch (actualRangedLevel)
        {
            case 1:
                extraRangedDamage = 5;
                _rangedCooldown = 1f;
                break;
            case 2:
                extraRangedDamage = 7;
                _rangedCooldown = 1f;
                break;
            case 3:
                extraRangedDamage = 9;
                _rangedCooldown = 1f;
                break;
            case 4:
                extraRangedDamage = 9;
                _rangedCooldown = 1f;
                break;
            case 5:
                extraRangedDamage = 13;
                _rangedCooldown = 1f;
                break;
            case 6:
                extraRangedDamage = 13;
                _rangedCooldown = 1f;
                break;
            case 7:
                extraRangedDamage = 17;
                _rangedCooldown = 1f;
                break;
            case 8:
                extraRangedDamage = 19;
                _rangedCooldown = 1f;
                break;
        }
    }

    public void EngineerLevelUp()
    {
        actualEngineerLevel++;
        switch (actualEngineerLevel)
        {
            case 1:
                extraOrbitalDamage = 3;
                _orbitalCooldown = 15f;
                _orbitalDuration = 4f;
                break;
            case 2:
                extraOrbitalDamage = 3;
                _orbitalCooldown = 15f;
                _orbitalDuration = 5f;
                break;
            case 3:
                extraOrbitalDamage = 3;
                _orbitalCooldown = 13f;
                _orbitalDuration = 5f;
                break;
            case 4:
                extraOrbitalDamage = 6;
                _orbitalCooldown = 13f;
                _orbitalDuration = 5f;
                break;
            case 5:
                extraOrbitalDamage = 6;
                _orbitalCooldown = 13f;
                _orbitalDuration = 6f;
                break;
            case 6:
                extraOrbitalDamage = 6;
                _orbitalCooldown = 11f;
                _orbitalDuration = 6f;
                break;
            case 7:
                extraOrbitalDamage = 12;
                _orbitalCooldown = 11f;
                _orbitalDuration = 6f;
                break;
            case 8:
                extraOrbitalDamage = 24;
                _orbitalCooldown = 10f;
                _orbitalDuration = 8f;
                break;
        }
    }
    public void BoomerangLevelUp()
    {
        actualBoomerangLevel++;
        switch (actualBoomerangLevel)
        {
            case 1:
                extraBoomerangDamage = 5;
                _boomerangCooldown = 1f;
                break;
            case 2:
                extraBoomerangDamage = 7;
                _boomerangCooldown = 1f;
                break;
            case 3:
                extraBoomerangDamage = 9;
                _boomerangCooldown = 1f;
                break;
            case 4:
                extraBoomerangDamage = 9;
                _boomerangCooldown = 1f;
                break;
            case 5:
                extraBoomerangDamage = 13;
                _boomerangCooldown = 1f;
                break;
            case 6:
                extraBoomerangDamage = 13;
                _boomerangCooldown = 1f;
                break;
            case 7:
                extraBoomerangDamage = 17;
                _boomerangCooldown = 1f;
                break;
            case 8:
                extraBoomerangDamage = 19;
                _boomerangCooldown = 1f;
                break;
        }
    }
    public void CurveSwordLevelUp()
    {
        actualCurveSwordLevel++;
        switch (actualCurveSwordLevel)
        {
            case 1:
                extraCurveSwordDamage = 3;
                _curveSwordCooldown = 25f;
                _curveSwordDuration = 4f;
                break;
            case 2:
                extraCurveSwordDamage = 3;
                _curveSwordCooldown = 25f;
                _curveSwordDuration = 5f;
                break;
            case 3:
                extraCurveSwordDamage = 3;
                _curveSwordCooldown = 23f;
                _curveSwordDuration = 5f;
                break;
            case 4:
                extraCurveSwordDamage = 6;
                _curveSwordCooldown = 23f;
                _curveSwordDuration = 5f;
                break;
            case 5:
                extraCurveSwordDamage = 6;
                _curveSwordCooldown = 21f;
                _curveSwordDuration = 6f;
                break;
            case 6:
                extraCurveSwordDamage = 6;
                _curveSwordCooldown = 21f;
                _curveSwordDuration = 6f;
                break;
            case 7:
                extraCurveSwordDamage = 12;
                _curveSwordCooldown = 19f;
                _curveSwordDuration = 6f;
                break;
            case 8:
                extraCurveSwordDamage = 24;
                _curveSwordCooldown = 18f;
                _curveSwordDuration = 8f;
                break;
        }
    }

    public void ResetTemporalStats()
    {
        actualMeleeLevel = 0;
        actualRangedLevel = 0;
        actualEngineerLevel = 0;
        actualBoomerangLevel = 0;
        actualCurveSwordLevel = 0;
    }


}
