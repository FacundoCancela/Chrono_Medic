using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgrade : MonoBehaviour
{
    [SerializeField] GameDataController dataController;
    [SerializeField] PlayerStats shopStats;

    public static event Action OnHealthUpgraded;
    public static event Action OnInjectionsLimitUpgraded;
    public static event Action OnInjectionsHealUpgraded;


    public void addHealth()
    {
        dataController.IncreaseHealth(shopStats.ExtraBuyHealth);
        OnHealthUpgraded?.Invoke();
    }

    public void addInjections()
    {
        dataController.IncreaseInjectionLimit(shopStats.ExtraInjectionsLimit);
        OnInjectionsLimitUpgraded?.Invoke();
    }

    public void extraInjectionsHeal()
    {
        dataController.IncreaseInjectionHeal(shopStats.ExtraInjectionHeal);
        OnInjectionsHealUpgraded?.Invoke();
    }
}
