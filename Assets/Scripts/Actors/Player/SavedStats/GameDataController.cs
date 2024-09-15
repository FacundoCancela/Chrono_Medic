using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class GameDataController : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public PlayerStats playerStats;

    public string savedFile;
    public GameData gameData = new GameData();

    public static GameDataController Instance
    {
        get { return instance; }
    }
    private static GameDataController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        savedFile = Application.dataPath + "/gameData.json";
        LoadData();
    }

    //Probar cambiar onDestroy por sceneLoaded
    private void OnDestroy()
    {
        SaveData();
    }

    private void LoadData()
    {
        if (File.Exists(savedFile))
        {
            string data = File.ReadAllText(savedFile);
            gameData = JsonConvert.DeserializeObject<GameData>(data);

            UpdatePlayerStats();
        }
    }

    public void SaveData()
    {
        GameData newData = new GameData()
        {
            maxHealth = gameData.maxHealth,
            damageMultiplier = gameData.damageMultiplier,
            money = gameData.money,
            upgradeCost = gameData.upgradeCost,
            InjectionHeal = gameData.InjectionHeal,
            InjectionsLimit = gameData.InjectionsLimit,
        };

        string jsonString = JsonConvert.SerializeObject(newData, Formatting.Indented);
        File.WriteAllText(savedFile, jsonString);

        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {
        player.UpdateClassStats();
    }

    public void IncreaseHealth(int moreHealth)
    {
        if (gameData.maxHealth >= playerStats.maxBuyHealth)
        {
            Debug.Log("Vida máxima alcanzada");
        }
        else if (gameData.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.maxHealth += moreHealth;
            SaveData();
            player.UpdateActualHealth();
        }
        else Debug.Log("Te falta plata");
    }

    public void IncreaseInjectionHeal(int extraHeal)
    {
        if (gameData.InjectionHeal >= playerStats.maxInjectionsHeal)
        {
            Debug.Log("Vida máxima alcanzada");
        }
        else if (gameData.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.InjectionHeal += extraHeal;
            SaveData();
        }
        else Debug.Log("Te falta plata");
    }

    public void IncreaseInjectionLimit(int moreInjections)
    {
        if (gameData.InjectionsLimit >= playerStats.maxInjectionsLimit)
        {
            Debug.Log("Vida máxima alcanzada");
        }
        else if (gameData.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.InjectionsLimit += moreInjections;
            SaveData();
        }
        else Debug.Log("Te falta plata");
    }

    public void IncreaseDamage(int moreDamage)
    {
        if (gameData.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.damageMultiplier += moreDamage;
            SaveData();
        }
        else Debug.Log("Te falta plata");
    }

    public void IncreaseMoney(int moreMoney)
    {
        gameData.money += moreMoney;
    }

    public void DecreaseMoney(int spentMoney)
    {
        gameData.money -= spentMoney;
        SaveData();
    }

    public void basicUpgradePrice()
    {
        gameData.upgradeCost = 50;
        SaveData();
    }
}
