using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class GameDataController : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public PlayerStats baseStats;

    public string savedFile;
    public Button maxHealthButton;
    public Button maxInjectionsButton;
    public Button maxInjectionHealButton;
    public GameData gameData = new GameData();

    public static event Action OnMoneyChanged;

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

        EnemyModel.OnEnemyDeath += HandleEnemyDeath;
        BossModel.OnBossDeath += HandleEnemyDeath;
    }

    private void OnDestroy()
    {
        EnemyModel.OnEnemyDeath -= HandleEnemyDeath;
        BossModel.OnBossDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(EnemyDeathData deathData)
    {
        IncreaseMoney(deathData.MoneyDropped);
    }

    public void Update()
    {
        if (baseStats.maxHealth == baseStats.maxBuyHealth && maxHealthButton != null)
        {
            maxHealthButton.interactable = false;
        }
        if (baseStats.ActualInjectionHeal == baseStats.maxInjectionsHeal && maxInjectionHealButton != null)
        {
            maxInjectionHealButton.interactable = false;
        }
        if (baseStats.ActualInjectionsLimit == baseStats.maxInjectionsLimit && maxInjectionsButton != null)
        {
            maxInjectionsButton.interactable = false;
        }
    }
    private void LoadData()
    {
        if(File.Exists(savedFile))
        {
            string data = File.ReadAllText(savedFile);
            gameData = JsonConvert.DeserializeObject<GameData>(data);

            baseStats.maxHealth = gameData.maxHealth;
            baseStats.damageMultiplier = gameData.damageMultiplier;
            baseStats.defensePercentage = gameData.defensePercentage;
            baseStats.money = gameData.money;
            baseStats.upgradeCost = gameData.upgradeCost;
            baseStats.ActualInjectionHeal = gameData.InjectionHeal;
            baseStats.ActualInjectionsLimit = gameData.InjectionsLimit;
            baseStats.GameStarted = gameData.GameStarted;
            
            if(player  != null)
                player.UpdateStats(baseStats);
        }
    }
    public void SaveData()
    {
        GameData newData = new GameData()
        {
            maxHealth = baseStats.maxHealth,
            damageMultiplier = baseStats.damageMultiplier,
            defensePercentage = baseStats.defensePercentage,
            money = baseStats.money,
            upgradeCost = baseStats.upgradeCost,
            InjectionHeal = baseStats.ActualInjectionHeal,
            InjectionsLimit = baseStats.ActualInjectionsLimit,
            GameStarted = baseStats.GameStarted,
            
        };

        string jsonString = JsonConvert.SerializeObject(newData, Formatting.Indented);
        File.WriteAllText(savedFile, jsonString);

    }

    public void IncreaseHealth(int moreHealth)
    {
        if(baseStats.maxHealth >= baseStats.maxBuyHealth)
        {
            Debug.Log("LimitHealth");
        }
        else if (baseStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.maxHealth += moreHealth;
            baseStats.maxHealth = gameData.maxHealth;
            player.UpdateHealth(baseStats);
            SaveData();
        }
        

    }

    public void IncreaseInjectionHeal(int extraHeal)
    {
        if (baseStats.ActualInjectionHeal >= baseStats.maxInjectionsHeal)
        {
            Debug.Log("LimitHealth");
        }
        else if (baseStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.InjectionHeal += extraHeal;
            baseStats.ActualInjectionHeal = gameData.InjectionHeal;
            player.UpdateStats(baseStats);
            SaveData();
        }
        

    }

    public void IncreaseInjectionLimit(int moreInjections)
    {
        if (baseStats.ActualInjectionsLimit >= baseStats.maxInjectionsLimit)
        {
            Debug.Log("LimitHealth");
        }
        else if (baseStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.InjectionsLimit += moreInjections;
            baseStats.ActualInjectionsLimit = gameData.InjectionsLimit;
            player.UpdateStats(baseStats);
            SaveData();
        }
        

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
        
    }
    
    public void IncreaseMoney(int moreMoney)
    {
        gameData.money += moreMoney;
        baseStats.money = gameData.money;
        SaveData();
        OnMoneyChanged?.Invoke();
    }

    public void DecreaseMoney(int spentMoney)
    {
        gameData.money -= spentMoney;
        baseStats.money = gameData.money;
        player.UpdateStats(baseStats);
        SaveData();
        OnMoneyChanged?.Invoke();
    }

    public void BasicUpgradePrice()
    {
        gameData.upgradeCost = 50;
        baseStats.upgradeCost = gameData.upgradeCost;
        player.UpdateStats(baseStats);
        SaveData();
        
    }

    public bool gameStarted()
    {
        return gameData.GameStarted;
    }

    public void StartGame()
    {
        gameData.GameStarted = true;
        baseStats.GameStarted = true;
        SaveData();
    }


}
