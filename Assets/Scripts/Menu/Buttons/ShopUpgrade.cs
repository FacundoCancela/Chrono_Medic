using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgrade : MonoBehaviour
{
    [SerializeField] GameDataController dataController;
    [SerializeField] PlayerStats shopStats;


    public void addHealth()
    {
        dataController.IncreaseHealth(shopStats.ExtraBuyHealth);
    }

    public void addInjections()
    {
        dataController.IncreaseInjectionLimit(shopStats.ExtraInjectionsLimit);
    }

    public void extraInjectionsHeal()
    {
        dataController.IncreaseInjectionHeal(shopStats.ExtraInjectionHeal);
    }
}
