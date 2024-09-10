using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    private GameData gameData;

    private void Update()
    {
        gameData = GameDataController.Instance.gameData;
        text.text = gameData.money.ToString();
    }
}
