using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUPScreen : MonoBehaviour
{
    [SerializeField] ExperienceManager experienceManager;

    public void Heal()
    {
        experienceManager.Heal();
        gameObject.SetActive(false);
    }

    public void MeleeDamage()
    {
        experienceManager.MeleeDamage();
        gameObject.SetActive(false);
    }

    public void RangedDamage()
    {
        experienceManager.RangedDamage();
        gameObject.SetActive(false);
    }
}
