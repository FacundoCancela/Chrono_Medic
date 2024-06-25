using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgrade : MonoBehaviour
{
    [SerializeField] GameDataController dataController;
    [SerializeField] public int extraHealthPerLevel;
    [SerializeField] public int extraInjectionLimit;
    [SerializeField] public int extraInjectionHeal;


    [SerializeField] public int basicSlashPrice;
    [SerializeField] public int bigSlashPrice;
    [SerializeField] public int orbitalWeaponPrice;

    public void addHealth()
    {
        dataController.IncreaseHealth(extraHealthPerLevel);
    }

    public void addInjections()
    {
        dataController.IncreaseInjectionLimit(extraInjectionLimit);
    }

    public void extraInjectionsHeal()
    {
        dataController.IncreaseInjectionHeal(extraInjectionHeal);
    }

    public void unlockBasicSlash()
    {
        dataController.UnlockBasicSlash(basicSlashPrice);
    }

    public void unlockBigSlash()
    {
        dataController.UnlockBigSlash(bigSlashPrice);
    }

    public void unlockCircleSlash()
    {
        dataController.UnlockOrbitalWeapon(orbitalWeaponPrice);
    }

}
