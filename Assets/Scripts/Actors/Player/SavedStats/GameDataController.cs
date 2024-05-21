using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    

public class GameDataController : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public PlayerStats playerStats;

    [SerializeField] public int upgradeCost = 50;

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
            playerStats.damageMultiplier = gameData.damage;
            playerStats.money = gameData.money;
            
            player.UpdateStats(playerStats);

            Debug.Log("vida maxima :" + gameData.maxHealth);
            Debug.Log("daño actual :" + gameData.damage);
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
            damage = playerStats.damageMultiplier,
            money = playerStats.money
        };

        string JSONstring = JsonUtility.ToJson(newData);

        File.WriteAllText(savedFile, JSONstring);

    }

    public void IncreaseHealth(int moreHealth)
    {
        if (playerStats.money >= upgradeCost)
        {
            DecreaseMoney(upgradeCost);
            gameData.maxHealth += moreHealth;
            playerStats.maxHealth = gameData.maxHealth;
            player.UpdateStats(playerStats);
            SaveData();
        }
        else Debug.Log("te falta plata");

    }
    
    public void IncreaseDamage(int moreDamage)
    {
        if(playerStats.money >= upgradeCost)
        {
            DecreaseMoney(upgradeCost);
            gameData.damage += moreDamage;
            playerStats.damageMultiplier = gameData.damage;
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

    public void DecreaseMoney(int moreMoney)
    {
        gameData.money -= moreMoney;
        playerStats.money = gameData.money;
        player.UpdateStats(playerStats);
        SaveData();
    }

}
