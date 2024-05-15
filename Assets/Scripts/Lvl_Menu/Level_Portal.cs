using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level_Portal : MonoBehaviour
{
    public SceneManager playButton;

    [SerializeField] private string sceneName;
    [SerializeField] private bool portalUnlocked;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (portalUnlocked)
        {
            rend.material.color = Color.green;
        }

        if (!portalUnlocked)
        {
            rend.material.color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && portalUnlocked)
        {
            playButton.CambiarEscena(sceneName);
        }
    }
}
