using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level_Portal : MonoBehaviour
{
    public SceneChanger playButton;

    [SerializeField] private string sceneName;
    [SerializeField] private bool portalUnlocked;

    [SerializeField] Sprite unlockedPortal;
    [SerializeField] Sprite lockedPortal;

    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (portalUnlocked)
        {
            spriteRenderer.sprite = unlockedPortal;

        }

        if (!portalUnlocked)
        {
            spriteRenderer.sprite = lockedPortal;
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
