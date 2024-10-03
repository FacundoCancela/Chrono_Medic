using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePrice : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI InjectionHealText;
    public TextMeshProUGUI InjectionLimitText;
    [SerializeField] ShopUpgrade shopUpgrade;
    [SerializeField] PlayerStats baseStats;

    private void Update()
    {
        HealthPrice();
        InjectionHealPrice();
        InjectionLimitPrice();
    }

    public void HealthPrice()
    {
        if(baseStats.maxHealth >= baseStats.maxBuyHealth)
        {
            healthText.text = ("Max Level");
        }
        else
        {
            healthText.text = ("$" + baseStats.upgradeCost + "/+" + shopUpgrade.extraHealthPerLevel);
        }
    }

    public void InjectionHealPrice()
    {
        if (baseStats.InjectionHeal >= baseStats.maxInjectionsHeal)
        {
            InjectionHealText.text = ("Max Level");
        }
        else
        {
            InjectionHealText.text = ("$" + baseStats.upgradeCost + "/+" + shopUpgrade.extraInjectionHeal);
        }
    }

    public void InjectionLimitPrice()
    {
        if (baseStats.InjectionsLimit >= baseStats.maxInjectionsLimit)
        {
            InjectionLimitText.text = ("Max Level");
        }
        else
        {
            InjectionLimitText.text = ("$" + baseStats.upgradeCost + "/+" + shopUpgrade.extraInjectionLimit);
        }
    }
}
