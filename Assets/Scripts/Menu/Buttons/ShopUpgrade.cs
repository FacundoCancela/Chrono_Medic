using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class ShopUpgrade : MonoBehaviour
{
    [SerializeField] GameDataController dataController;
    [SerializeField] public int extraHealthPerLevel;
    [SerializeField] public int extraDamagePerLevel;
    [SerializeField] public int extraMoney;

    [SerializeField] public int basicSlashPrice;
    [SerializeField] public int bigSlashPrice;
    [SerializeField] public int circleSlashPrice;

    public void addHealth()
    {
        dataController.IncreaseHealth(extraHealthPerLevel);
    }

    public void addDamage()
    {
        dataController.IncreaseDamage(extraDamagePerLevel);
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
        dataController.UnlockCircleSlash(circleSlashPrice);
    }

}
