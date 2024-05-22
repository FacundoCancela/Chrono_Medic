using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] LevelUPScreen levelUPScreen;
    [SerializeField] XPBar XpBar;

    public int actualExperience;
    public int maxExperience;
    public int extraExperiencePerLevel;
    public int level;

    private void Awake()
    {
        XpBar.SetMaxXP(maxExperience);
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

        actualExperience = 0;
        maxExperience += extraExperiencePerLevel;
        level++;
    }

    //Level up efects

    public void Heal()
    {

    }

    public void MeleeDamage()
    {

    }

    public void RangedDamage()
    {

    }


}
