using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePrice : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI InjectionHealText;
    public TextMeshProUGUI InjectionLimitText;
    public TextMeshProUGUI BasicSlashText;
    public TextMeshProUGUI BigSlashText;
    public TextMeshProUGUI circleSlashText;
    [SerializeField] ShopUpgrade shopUpgrade;
    [SerializeField] PlayerStats playerStats;

    private void Update()
    {
        HealthPrice();
        InjectionHealPrice();
        InjectionLimitPrice();
        BasicSlashUnlocked();
        BigSlashUnlocked();
        OrbitalWeaponUnlocked();
    }

    public void HealthPrice()
    {
        if(playerStats.maxHealth >= playerStats.maxBuyHealth)
        {
            healthText.text = ("Max Level");
        }
        else
        {
            healthText.text = ("$" + playerStats.upgradeCost + "/+" + shopUpgrade.extraHealthPerLevel);
        }
    }

    public void InjectionHealPrice()
    {
        if (playerStats.InjectionHeal >= playerStats.maxInjectionsHeal)
        {
            InjectionHealText.text = ("Max Level");
        }
        else
        {
            InjectionHealText.text = ("$" + playerStats.upgradeCost + "/+" + shopUpgrade.extraInjectionHeal);
        }
    }

    public void InjectionLimitPrice()
    {
        if (playerStats.InjectionsLimit >= playerStats.maxInjectionsLimit)
        {
            InjectionLimitText.text = ("Max Level");
        }
        else
        {
            InjectionLimitText.text = ("$" + playerStats.upgradeCost + "/+" + shopUpgrade.extraInjectionLimit);
        }
    }

    public void BasicSlashUnlocked()
    {
        if (playerStats.basicSlashUnlocked)
        {
            BasicSlashText.text = ("Unlocked");
        }
        else BasicSlashText.text = ("$:" + shopUpgrade.basicSlashPrice);
    }

    public void BigSlashUnlocked()
    {
        if (playerStats.bigSlashUnlocked)
        {
            BigSlashText.text = ("Unlocked");
        }
        else BigSlashText.text = ("$:" + shopUpgrade.bigSlashPrice);
    }

    public void OrbitalWeaponUnlocked()
    {
        if (playerStats.orbitalWeaponUnlocked)
        {
            circleSlashText.text = ("Unlocked");
        }
        else circleSlashText.text = ("$:" + shopUpgrade.orbitalWeaponPrice);
    }


}
