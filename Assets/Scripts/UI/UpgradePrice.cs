using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePrice : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI InjectionHealText;
    public TextMeshProUGUI InjectionLimitText;
    [SerializeField] PlayerStats baseStats;

    private void OnEnable()
    {
        ShopUpgrade.OnHealthUpgraded += HealthPrice;
        ShopUpgrade.OnInjectionsLimitUpgraded += InjectionLimitPrice;
        ShopUpgrade.OnInjectionsHealUpgraded += InjectionHealPrice;
        HealthPrice();
        InjectionLimitPrice();
        InjectionHealPrice();
    }
    private void OnDisable()
    {
        ShopUpgrade.OnHealthUpgraded -= HealthPrice;
        ShopUpgrade.OnInjectionsLimitUpgraded -= InjectionLimitPrice;
        ShopUpgrade.OnInjectionsHealUpgraded -= InjectionHealPrice;
    }


    public void HealthPrice()
    {
        if(baseStats.maxHealth >= baseStats.maxBuyHealth)
        {
            healthText.text = ("Max Level");
        }
        else
        {
            healthText.text = ("$" + baseStats.upgradeCost + "/+" + baseStats.ExtraBuyHealth);
        }
    }

    public void InjectionHealPrice()
    {
        if (baseStats.ActualInjectionHeal >= baseStats.maxInjectionsHeal)
        {
            InjectionHealText.text = ("Max Level");
        }
        else
        {
            InjectionHealText.text = ("$" + baseStats.upgradeCost + "/+" + baseStats.ExtraInjectionHeal);
        }
    }

    public void InjectionLimitPrice()
    {
        if (baseStats.ActualInjectionsLimit >= baseStats.maxInjectionsLimit)
        {
            InjectionLimitText.text = ("Max Level");
        }
        else
        {
            InjectionLimitText.text = ("$" + baseStats.upgradeCost + "/+" + baseStats.ExtraInjectionsLimit);
        }
    }
}
