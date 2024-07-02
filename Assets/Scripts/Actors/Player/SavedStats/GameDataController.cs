using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class GameDataController : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public PlayerStats playerStats;
    [SerializeField] public UpgradePrice upgradePrice;
    

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

    private void LoadData()
    {
        if(File.Exists(savedFile))
        {
            string data = File.ReadAllText(savedFile);
            gameData = JsonConvert.DeserializeObject<GameData>(data);

            playerStats.maxHealth = gameData.maxHealth;
            playerStats.damageMultiplier = gameData.damageMultiplier;
            playerStats.money = gameData.money;
            playerStats.upgradeCost = gameData.upgradeCost;
            playerStats.InjectionHeal = gameData.InjectionHeal;
            playerStats.InjectionsLimit = gameData.InjectionsLimit;
            
            player.UpdateStats(playerStats);
        }
    }

    private void SaveData()
    {
        GameData newData = new GameData()
        {
            maxHealth = playerStats.maxHealth,
            damageMultiplier = playerStats.damageMultiplier,
            money = playerStats.money,
            upgradeCost = playerStats.upgradeCost,
            InjectionHeal = playerStats.InjectionHeal,
            InjectionsLimit = playerStats.InjectionsLimit,
            
        };

        string jsonString = JsonConvert.SerializeObject(newData, Formatting.Indented);
        File.WriteAllText(savedFile, jsonString);

    }

    public void IncreaseHealth(int moreHealth)
    {
        if(playerStats.maxHealth >= playerStats.maxBuyHealth)
        {
            Debug.Log("Vida maxima alcanzada");
        }
        else if (playerStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.maxHealth += moreHealth;
            playerStats.maxHealth = gameData.maxHealth;
            player.UpdateHealth(playerStats);
            SaveData();
        }
        else Debug.Log("te falta plata");

    }

    public void IncreaseInjectionHeal(int extraHeal)
    {
        if (playerStats.InjectionHeal >= playerStats.maxInjectionsHeal)
        {
            Debug.Log("Vida maxima alcanzada");
        }
        else if (playerStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.InjectionHeal += extraHeal;
            playerStats.InjectionHeal = gameData.InjectionHeal;
            player.UpdateStats(playerStats);
            SaveData();
        }
        else Debug.Log("te falta plata");

    }

    public void IncreaseInjectionLimit(int moreInjections)
    {
        if (playerStats.InjectionsLimit >= playerStats.maxInjectionsLimit)
        {
            Debug.Log("Vida maxima alcanzada");
        }
        else if (playerStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.InjectionsLimit += moreInjections;
            playerStats.InjectionsLimit = gameData.InjectionsLimit;
            player.UpdateStats(playerStats);
            SaveData();
        }
        else Debug.Log("te falta plata");

    }

    public void IncreaseDamage(int moreDamage)
    {
        if(playerStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.damageMultiplier += moreDamage;
            playerStats.damageMultiplier = gameData.damageMultiplier;
            player.UpdateStats(playerStats);
            SaveData();
        }
        else Debug.Log("te falta plata");
    }
    
    public void IncreaseMoney(int moreMoney)
    {
        gameData.money += moreMoney;
        playerStats.money = gameData.money;
        player.UpdateStats(playerStats);
        SaveData();
    }

    public void DecreaseMoney(int spentMoney)
    {
        gameData.money -= spentMoney;
        playerStats.money = gameData.money;
        player.UpdateStats(playerStats);
        SaveData();
    }

    public void basicUpgradePrice()
    {
        gameData.upgradeCost = 50;
        playerStats.upgradeCost = gameData.upgradeCost;
        player.UpdateStats(playerStats);
        SaveData();
        
    }
}
