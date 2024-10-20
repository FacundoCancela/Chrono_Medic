using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerStats playerStats;
    public TextMeshProUGUI injectionsText;

    public int injections;
    public bool _isInCombat = false;

    private void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Lvl_Menu")
        {
            _isInCombat = false;
        }
        else
        {
            _isInCombat = true;
        }

        playerController = GetComponent<PlayerController>();

    }


    private void Update()
    {
        if(injectionsText != null)
        injectionsText.text = (injections.ToString() + "/" + playerStats.ActualInjectionsLimit);
    }

    public void saveInjection()
    {
        if (injections < playerStats.ActualInjectionsLimit)
        {
            injections++;
        }        
    }

    public void useInjection()
    {
        if (injections > 0 && _isInCombat) 
        {
            injections--;
            float healAmount = (playerStats.maxHealth * playerStats.ActualInjectionHeal / 100);
            playerController.GetHealed((int)healAmount);
        }
    }

    

}
