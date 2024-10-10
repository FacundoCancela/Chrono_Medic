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

    [Header("Weapon scriptable objects")]

    [SerializeField] ClassManager classManager;
    [SerializeField] WeaponStatsPerLevel meleeWeapon;
    [SerializeField] WeaponStatsPerLevel rangeWeapon;
    [SerializeField] WeaponStatsPerLevel engenieerWeapon;
    [SerializeField] WeaponStatsPerLevel BoomerangWeapon;
    [SerializeField] WeaponStatsPerLevel CurveWeapon;

    [Header("Melee")]
    public int actualMeleeLevel = 0;
    public int meleeDamage = 1;
    public float meleeCooldown = 1f;
    [Header("Ranged")]
    public int actualRangedLevel = 0;
    public int rangedDamage = 1;
    public float rangedCooldown = 1f;
    [Header("Engineer")]
    public int actualEngineerLevel = 0;
    public int engineerDamage = 1;
    public float orbitalCooldown = 1f;
    public float orbitalDuration = 5f;
    public int orbitalSpeed = 100;
    public int orbitalRange = 1;
    public int numberOfOrbs = 1;
    [Header("Curve")]
    public int actualCurveSwordLevel = 0;
    public int curveSwordDamage = 1;
    public float curveSwordCooldown = 4f;
    public float curveSwordDuration = 4f;
    public int curveSwordSpeed = 1000;
    [Header("Boomerang")]
    public int actualBoomerangLevel = 0;
    public int boomerangDamage = 1;
    public float boomerangCooldown = 1f;


    private void Awake()
    {
        XpBar.SetMaxXP(maxExperience);
    }

    private void Start()
    {
        ResetTemporalStats();

        switch (ClassManager.currentClass)
        {
            case ClassManager.SelectedClass.Melee:
                MeleeLevelUp();
                break;
            case ClassManager.SelectedClass.Ranged:
                RangedLevelUp();
                break;
            case ClassManager.SelectedClass.Engineer:
                EngineerLevelUp();
                break;
        }

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
        meleeDamage = meleeWeapon.weaponDamage[actualMeleeLevel];
        meleeCooldown = meleeWeapon.cooldown[actualMeleeLevel];
    }

    public void RangedLevelUp()
    {
        actualRangedLevel++;
        rangedDamage = rangeWeapon.weaponDamage[actualRangedLevel];
        rangedCooldown = rangeWeapon.cooldown[actualRangedLevel];
    }

    public void EngineerLevelUp()
    {
        actualEngineerLevel++;
        engineerDamage = engenieerWeapon.weaponDamage[actualEngineerLevel];
        orbitalCooldown = engenieerWeapon.cooldown[actualEngineerLevel];
        orbitalDuration = engenieerWeapon.duration[actualEngineerLevel];
        orbitalSpeed = engenieerWeapon.rotationSpeed[actualEngineerLevel];
        orbitalRange = engenieerWeapon.rotationRange[actualEngineerLevel];
        numberOfOrbs = engenieerWeapon.numberOfOrbs[actualEngineerLevel];
    }

    public void BoomerangLevelUp()
    {
        actualBoomerangLevel++;
        boomerangDamage = BoomerangWeapon.weaponDamage[actualBoomerangLevel];
        boomerangCooldown = BoomerangWeapon.cooldown[actualBoomerangLevel];
    }
    public void CurveSwordLevelUp()
    {
        actualCurveSwordLevel++;
        curveSwordDamage = CurveWeapon.weaponDamage[actualCurveSwordLevel];
        curveSwordCooldown = CurveWeapon.cooldown[actualCurveSwordLevel];
        curveSwordDuration = CurveWeapon.duration[actualCurveSwordLevel];
        curveSwordSpeed = CurveWeapon.rotationSpeed[actualCurveSwordLevel];
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
