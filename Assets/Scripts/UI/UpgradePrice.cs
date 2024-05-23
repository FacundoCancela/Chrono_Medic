using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePrice : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI BasicSlashText;
    public TextMeshProUGUI BigSlashText;
    public TextMeshProUGUI circleSlashText;
    [SerializeField] ShopUpgrade shopUpgrade;
    [SerializeField] PlayerStats playerStats;

    private void Update()
    {
        HealthPrice();
        DamagePrice();
        BasicSlashUnlocked();
        BigSlashUnlocked();
        CircleSlashUnlocked();
    }

    public void HealthPrice()
    {
        healthText.text = ("$" + playerStats.upgradeCost + "/+" +shopUpgrade.extraHealthPerLevel);
    }

    public void DamagePrice()
    {
        damageText.text = ("$" + playerStats.upgradeCost + "/+" + shopUpgrade.extraDamagePerLevel);
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

    public void CircleSlashUnlocked()
    {
        if (playerStats.circleSlashUnlocked)
        {
            circleSlashText.text = ("Unlocked");
        }
        else circleSlashText.text = ("$:" + shopUpgrade.circleSlashPrice);
    }


}
