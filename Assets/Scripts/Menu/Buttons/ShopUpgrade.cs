using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgrade : MonoBehaviour
{
    [SerializeField] GameDataController dataController;
    [SerializeField] public int extraHealthPerLevel;
    [SerializeField] public int extraInjectionLimit;
    [SerializeField] public int extraInjectionHeal;


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
}
