using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] public PlayerStats playerStats;

    private void OnEnable()
    {
        GameDataController.OnMoneyChanged += UpdateMoney;
        UpdateMoney();
    }
    private void OnDisable()
    {
        GameDataController.OnMoneyChanged -= UpdateMoney;
    }

    private void UpdateMoney()
    {
        text.text = ("" + playerStats.money);
    }

}
