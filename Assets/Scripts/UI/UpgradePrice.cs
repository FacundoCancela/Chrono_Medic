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

    private GameDataController gameDataController;
    private GameData gameData;

    private void Awake()
    {
        gameDataController = GameDataController.Instance;
    }

    private void Update()
    {
        gameData = gameDataController.gameData;
        HealthPrice();
        InjectionHealPrice();
        InjectionLimitPrice();
    }

    public void HealthPrice()
    {
        if (gameData.maxHealth >= gameDataController.playerStats.maxBuyHealth)
        {
            healthText.text = ("Max Level");
        }
        else
        {
            healthText.text = ("$" + gameData.upgradeCost + "/+" + shopUpgrade.extraHealthPerLevel);
        }
    }

    public void InjectionHealPrice()
    {
        if (gameData.InjectionHeal >= gameDataController.playerStats.maxInjectionsHeal)
        {
            InjectionHealText.text = ("Max Level");
        }
        else
        {
            InjectionHealText.text = ("$" + gameData.upgradeCost + "/+" + shopUpgrade.extraInjectionHeal);
        }
    }

    public void InjectionLimitPrice()
    {
        if (gameData.InjectionsLimit >= gameDataController.playerStats.maxInjectionsLimit)
        {
            InjectionLimitText.text = ("Max Level");
        }
        else
        {
            InjectionLimitText.text = ("$" + gameData.upgradeCost + "/+" + shopUpgrade.extraInjectionLimit);
        }
    }
}
