using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    

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
            gameData = JsonUtility.FromJson<GameData>(data);

            playerStats.maxHealth = gameData.maxHealth;
            playerStats.damageMultiplier = gameData.damageMultiplier;
            playerStats.money = gameData.money;
            playerStats.basicSlashUnlocked = gameData.basicSlashUnlocked;
            playerStats.bigSlashUnlocked = gameData.bigSlashUnlocked;
            playerStats.circleSlashUnlocked = gameData.circleSlashUnlocked;
            playerStats.upgradeCost = gameData.upgradeCost;
            
            player.UpdateStats(playerStats);

            Debug.Log("vida maxima :" + gameData.maxHealth);
            Debug.Log("daño actual :" + gameData.damageMultiplier);
            Debug.Log("dinero :" + gameData.money);
        }
        else
        {
            Debug.Log("el archivo no existe");
        }
    }

    private void SaveData()
    {
        GameData newData = new GameData()
        {
            maxHealth = playerStats.maxHealth,
            damageMultiplier = playerStats.damageMultiplier,
            money = playerStats.money,
            basicSlashUnlocked = playerStats.basicSlashUnlocked,
            bigSlashUnlocked = playerStats.bigSlashUnlocked,
            circleSlashUnlocked = playerStats.circleSlashUnlocked,
            upgradeCost = playerStats.upgradeCost,
            
        };

        string JSONstring = JsonUtility.ToJson(newData);

        File.WriteAllText(savedFile, JSONstring);

    }

    public void IncreaseHealth(int moreHealth)
    {
        if (playerStats.money >= gameData.upgradeCost)
        {
            DecreaseMoney(gameData.upgradeCost);
            gameData.maxHealth += moreHealth;
            playerStats.maxHealth = gameData.maxHealth;
            player.UpdateStats(playerStats);
            SaveData();
            if(upgradePrice != null)
                upgradePrice.HealthPrice();
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
            if (upgradePrice != null)
                upgradePrice.DamagePrice();
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

    public void UnlockBasicSlash(int shopPrice)
    {
        if (playerStats.money >= shopPrice && !playerStats.basicSlashUnlocked)
        {
            DecreaseMoney(shopPrice);
            playerStats.basicSlashUnlocked = true;
            player.UpdateStats(playerStats);
            SaveData();
            if (upgradePrice != null)
                upgradePrice.BasicSlashUnlocked();
        }
        else if (playerStats.basicSlashUnlocked) Debug.Log("arma ya obtenida");
        else Debug.Log("te falta plata");
    }
    public void UnlockBigSlash(int shopPrice)
    {
        if (playerStats.money >= shopPrice && !playerStats.bigSlashUnlocked)
        {
            DecreaseMoney(shopPrice);
            playerStats.bigSlashUnlocked = true;
            player.UpdateStats(playerStats);
            SaveData();
            if (upgradePrice != null)
                upgradePrice.BigSlashUnlocked();
        }
        else if (playerStats.bigSlashUnlocked) Debug.Log("arma ya obtenida");
        else Debug.Log("te falta plata");
    }
    public void UnlockCircleSlash(int shopPrice)
    {
        if (playerStats.money >= shopPrice && !playerStats.circleSlashUnlocked)
        {
            DecreaseMoney(shopPrice);
            playerStats.circleSlashUnlocked = true;
            player.UpdateStats(playerStats);
            SaveData();
            if (upgradePrice != null)
                upgradePrice.CircleSlashUnlocked();
        }
        else if (playerStats.circleSlashUnlocked) Debug.Log("arma ya obtenida");
        else Debug.Log("te falta plata");
    }



}
