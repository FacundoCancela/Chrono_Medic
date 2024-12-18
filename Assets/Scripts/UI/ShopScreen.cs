using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] ShopUpgrade ShopUI;
    [SerializeField] PauseManager pauseManager;
    [SerializeField] public bool inShopZone = false;
    [SerializeField] public bool shopOpen = false;
    public GameObject interaccion;

    private void Update()
    {
        if (pauseManager != null && pauseManager.gamePaused)
        {
            return; 
        }


        if (Input.GetKeyDown(KeyCode.F) && inShopZone || (Input.GetKeyDown(KeyCode.Escape) && shopOpen))
        {
            shopOpen = !shopOpen;
            if(shopOpen)
            {
                if (ShopUI != null)
                {
                    pauseManager.canPause = false;
                    Time.timeScale = 0f;
                    ShopUI.gameObject.SetActive(true);
                    interaccion.SetActive(false);
                }
            }
            else
            {
                pauseManager.canPause = true;
                Time.timeScale = 1f;
                ShopUI.gameObject.SetActive(false);
                interaccion.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (interaccion != null)
            {
                interaccion.SetActive(true);
            }
            inShopZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (interaccion != null)
            {
                interaccion.SetActive(false);
            }
            inShopZone = false;
        }
    }
}
