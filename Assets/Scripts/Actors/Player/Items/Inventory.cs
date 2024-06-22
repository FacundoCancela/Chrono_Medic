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

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        injectionsText.text = injections.ToString();
    }

    public void saveInjection()
    {
        injections++;
        injectionsText.text = injections.ToString();
    }

    public void useInjection()
    {
        if (injections > 0) 
        {
            injections--;
            injectionsText.text = injections.ToString();
            playerController.GetHealed(playerStats.InjectionHealth);
        }
    }

}
