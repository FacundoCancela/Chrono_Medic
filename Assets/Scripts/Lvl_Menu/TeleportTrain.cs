using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrain : MonoBehaviour
{
    
    [SerializeField] private Transform destination;
    [SerializeField] private WeaponManager weaponManager;
    private PlayerController _player;

    public GameObject interaccion;

    private bool isInteracting = false;



    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            isInteracting = false;
            interaccion.SetActive(false);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            isInteracting = true;
            interaccion.SetActive(true);

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInteracting)
        {
            
            _player.transform.position = destination.position;
            weaponManager._isInCombat = !weaponManager._isInCombat;
        }
    }

}
