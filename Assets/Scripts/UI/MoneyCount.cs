using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] public PlayerStats playerStats;

    private void Update()
    {
        text.text = ("" + playerStats.money);
    }
}
