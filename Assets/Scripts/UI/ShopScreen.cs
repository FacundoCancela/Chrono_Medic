using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] ShopUpgrade ShopUI;
    [SerializeField] PauseManager pauseManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(ShopUI != null)
            {
                pauseManager.canPause = false;
                ShopUI.gameObject.SetActive(true);
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ShopUI != null)
            {
                pauseManager.canPause = true;
                ShopUI.gameObject.SetActive(false);
            }
                
        }
    }
}
