using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgrade : MonoBehaviour
{
    [SerializeField] GameDataController dataController;
    [SerializeField] public int extraHealthPerLevel;
    private int extraMoney;

    [SerializeField] public int basicSlashPrice;
    [SerializeField] public int bigSlashPrice;
    [SerializeField] public int orbitalWeaponPrice;

    public void addHealth()
    {
        dataController.IncreaseHealth(extraHealthPerLevel);
    }

    public void addMoney()
    {
        dataController.IncreaseMoney(extraMoney);
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
