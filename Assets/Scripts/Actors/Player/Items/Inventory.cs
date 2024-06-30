using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerStats playerStats;
    public TextMeshProUGUI injectionsText;

    public int injections;

    private void Update()
    {
        injectionsText.text = (injections.ToString() + "/" + playerStats.InjectionsLimit);
    }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();        
    }

    public void saveInjection()
    {
        if (injections < playerStats.InjectionsLimit)
        {
            injections++;
        }        
    }

    public void useInjection()
    {
        if (injections > 0) 
        {
            injections--;
            float healAmount = (playerStats.maxHealth * playerStats.InjectionHeal / 100);
            playerController.GetHealed((int)healAmount);
        }
    }

    

}
