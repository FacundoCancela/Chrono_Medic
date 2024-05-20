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
            IncreaseHealth();
        }
    }

    private void LoadData()
    {
        if(File.Exists(savedFile))
        {
            string data = File.ReadAllText(savedFile);
            gameData = JsonUtility.FromJson<GameData>(data);

            playerStats.maxHealth = gameData.maxHealth;
            player.UpdateStats(playerStats);


            Debug.Log("vida maxima :" + gameData.maxHealth);
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
            maxHealth = playerStats.maxHealth
        };

        string JSONstring = JsonUtility.ToJson(newData);

        File.WriteAllText(savedFile, JSONstring);

        Debug.Log("Archivo guardado");
    }

    private void IncreaseHealth()
    {
        gameData.maxHealth += moreHealth;
        playerStats.maxHealth = gameData.maxHealth;
        player.UpdateStats(playerStats);
        SaveData();

        Debug.Log("vida gamedata:" + gameData.maxHealth);
    }

}
