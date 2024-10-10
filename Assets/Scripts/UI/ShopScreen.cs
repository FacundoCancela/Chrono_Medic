using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] ShopUpgrade ShopUI;
    [SerializeField] PauseManager pauseManager;
    [SerializeField] public bool inShopZone = false;
    [SerializeField] public bool shopOpen = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && inShopZone)
        {
            shopOpen = !shopOpen;
            if(shopOpen)
            {
                if (ShopUI != null)
                {
                    pauseManager.canPause = false;
                    Time.timeScale = 0f;
                    ShopUI.gameObject.SetActive(true);
                }
            }
            else
            {
                pauseManager.canPause = true;
                Time.timeScale = 1f;
                ShopUI.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inShopZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inShopZone = false;
        }
    }
}
