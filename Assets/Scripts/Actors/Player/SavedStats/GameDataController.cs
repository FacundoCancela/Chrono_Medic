using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class GameDataController : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public PlayerStats baseStats;
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

            baseStats.maxHealth = gameData.maxHealth;
            baseStats.damageMultiplier = gameData.damageMultiplier;
            baseStats.money = gameData.money;
            baseStats.upgradeCost = gameData.upgradeCost;
            baseStats.InjectionHeal = gameData.InjectionHeal;
            baseStats.InjectionsLimit = gameData.InjectionsLimit;
            
            player.UpdateStats(baseStats);
        }
    }
    public void SaveData()
    {
        GameData newData = new GameData()
        {
            maxHealth = baseStats.maxHealth,
            damageMultiplier = baseStats.damageMultiplier,
            money = baseStats.money,
            upgradeCost = baseStats.upgradeCost,
            InjectionHeal = baseStats.InjectionHeal,
            InjectionsLimit = baseStats.InjectionsLimit,
            
        };

        string jsonString = JsonConvert.SerializeObject(newData, Formatting.Indented);
        File.WriteAllText(savedFile, jsonString);

    }

    public void IncreaseHealth(int moreHealth)
    {
        if(baseStats.maxHealth >= baseStats.maxBuyHealth)
        {
            Debug.Log("Vida maxima alcanzada");
        }
        else if (baseStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.maxHealth += moreHealth;
            baseStats.maxHealth = gameData.maxHealth;
            player.UpdateHealth(baseStats);
            SaveData();
        }
        else Debug.Log("te falta plata");

    }

    public void IncreaseInjectionHeal(int extraHeal)
    {
        if (baseStats.InjectionHeal >= baseStats.maxInjectionsHeal)
        {
            Debug.Log("Vida maxima alcanzada");
        }
        else if (baseStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.InjectionHeal += extraHeal;
            baseStats.InjectionHeal = gameData.InjectionHeal;
            player.UpdateStats(baseStats);
            SaveData();
        }
        else Debug.Log("te falta plata");

    }

    public void IncreaseInjectionLimit(int moreInjections)
    {
        if (baseStats.InjectionsLimit >= baseStats.maxInjectionsLimit)
        {
            Debug.Log("Vida maxima alcanzada");
        }
        else if (baseStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.InjectionsLimit += moreInjections;
            baseStats.InjectionsLimit = gameData.InjectionsLimit;
            player.UpdateStats(baseStats);
            SaveData();
        }
        else Debug.Log("te falta plata");

    }

    public void IncreaseDamage(int moreDamage)
    {
        if(baseStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.damageMultiplier += moreDamage;
            baseStats.damageMultiplier = gameData.damageMultiplier;
            player.UpdateStats(baseStats);
            SaveData();
        }
        else Debug.Log("te falta plata");
    }
    
    public void IncreaseMoney(int moreMoney)
    {
        gameData.money += moreMoney;
        baseStats.money = gameData.money;
        player.UpdateStats(baseStats);
        SaveData();
    }

    public void DecreaseMoney(int spentMoney)
    {
        gameData.money -= spentMoney;
        baseStats.money = gameData.money;
        player.UpdateStats(baseStats);
        SaveData();
    }

    public void basicUpgradePrice()
    {
        gameData.upgradeCost = 50;
        baseStats.upgradeCost = gameData.upgradeCost;
        player.UpdateStats(baseStats);
        SaveData();
        
    }
}
