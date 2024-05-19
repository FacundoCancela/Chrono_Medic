using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStatsControler : MonoBehaviour
{
    public GameObject player;
    public string savedStats;
    public PlayerStats playerStats = new PlayerStats();

    private void Awake()
    {
        savedStats = Application.persistentDataPath + "/playerStats.json";

        player = GameObject.FindGameObjectWithTag("Player");

        LoadData();
    }

    private void Update()
    {
        // Puedes añadir cualquier lógica que necesites en el Update
    }

    private void LoadData()
    {
        if (File.Exists(savedStats))
        {
            string content = File.ReadAllText(savedStats);
            playerStats = JsonUtility.FromJson<PlayerStats>(content);
            Debug.Log("Datos cargados correctamente");
        }
        else
        {
            Debug.Log("No hay archivo de datos guardado");
        }
    }

    public void SaveData()
    {
        PlayerStats newData = new PlayerStats()
        {
            maxHealth = player.GetComponent<>().health,
        };
        

        string json = JsonUtility.ToJson(playerStats);
        File.WriteAllText(savedStats, json);
        Debug.Log("Datos guardados correctamente");
    }
}
