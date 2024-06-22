using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injection : MonoBehaviour
{
    public Inventory inventory;  
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            if(inventory != null)
            {
                inventory.saveInjection();
            }
            Destroy(gameObject);
        }
    }
}
