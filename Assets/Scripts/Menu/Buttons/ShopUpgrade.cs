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



}
