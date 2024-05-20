using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;    

public class GameDataController : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public PlayerStats playerStats;
    public string savedFile;
    public GameData gameData = new GameData();

    //PlayerStats
    public int moreHealth = 10;


    private void Awake()
    {
        savedFile = Application.dataPath + "/gameData.json";

        LoadData();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            LoadData();
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            IncreaseHealth(moreHealth);
        }
    }

    private void LoadData()
    {
        if(File.Exists(savedFile))
        {
            string data = File.ReadAllText(savedFile);
            gameData = JsonUtility.FromJson<GameData>(data);

            playerStats.maxHealth = gameData.maxHealth;
            playerStats.damage = gameData.damage;
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
            damage = playerStats.damage,
            money = playerStats.money
        };

        string JSONstring = JsonUtility.ToJson(newData);

        File.WriteAllText(savedFile, JSONstring);

        Debug.Log("Archivo guardado");
    }

    public void IncreaseHealth(int moreHealth)
    {
        gameData.maxHealth += moreHealth;
        playerStats.maxHealth = gameData.maxHealth;
        player.UpdateStats(playerStats);
        SaveData();
    }
    
    public void IncreaseDamage(int moreDamage)
    {
        gameData.damage += moreDamage;
        playerStats.damage = gameData.damage;
        player.UpdateStats(playerStats);
        SaveData();
    }
    
    public void IncreaseMoney(int moreMoney)
    {
        gameData.money += moreMoney;
        playerStats.money = gameData.money;
        player.UpdateStats(playerStats);
        SaveData();
    }

}
