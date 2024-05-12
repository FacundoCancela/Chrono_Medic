using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level_Portal : MonoBehaviour
{
    public PlayButton playButton;

    private string sceneName;

    private void Awake()
    {
        sceneName = gameObject.tag;
        //Debug.Log(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playButton.CambiarEscena(sceneName);
        }
    }
}
