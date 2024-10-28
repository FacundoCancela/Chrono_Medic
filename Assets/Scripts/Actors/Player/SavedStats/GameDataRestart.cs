using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class GameDataRestart : MonoBehaviour
{
    [SerializeField] private InitialGameData initialGameData;

    private string savedFile;

    private void Awake()
    {
        savedFile = Application.dataPath + "/gameData.json";
    }

    public void ResetGameData()
    {
        GameData newData = new GameData()
        {
            maxHealth = initialGameData.maxHealth,
            damageMultiplier = initialGameData.damageMultiplier,
            defensePercentage = initialGameData.defensePercentage,
            money = initialGameData.money,
            movementSpeed = initialGameData.movementSpeed,
            actualLevel = initialGameData.actualLevel,
            attackSpeed = initialGameData.attackSpeed,
            attackRange = initialGameData.attackRange,
            upgradeCost = initialGameData.upgradeCost,
            InjectionHeal = initialGameData.InjectionHeal,
            InjectionsLimit = initialGameData.InjectionsLimit
        };

        string jsonString = JsonConvert.SerializeObject(newData, Formatting.Indented);
        File.WriteAllText(savedFile, jsonString);

        Debug.Log("Game data has been reset to initial values and saved.");
    }
}
