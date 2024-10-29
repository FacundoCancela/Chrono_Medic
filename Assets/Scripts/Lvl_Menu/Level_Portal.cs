using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level_Portal : MonoBehaviour
{
    [SerializeField] public string sceneName;
    [SerializeField] private bool portalUnlocked;

    [SerializeField] Sprite unlockedPortal;
    [SerializeField] Sprite lockedPortal;

    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] ClassSelector classSelector;

    [SerializeField] PauseManager pauseManager;

    [SerializeField] public bool inPortalZone = false;
    public GameObject interaccion;

    Animator animator;

    private bool isInteracting = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (portalUnlocked)
        {
            spriteRenderer.sprite = unlockedPortal;
            animator.SetTrigger("Idle");
        }
        else
        {
            spriteRenderer.sprite = lockedPortal;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inPortalZone)
        {
            isInteracting = !isInteracting;
            interaccion.SetActive(false);
            if (isInteracting && classSelector != null)
            {
                classSelector.gameObject.SetActive(true);
                classSelector.sceneName = sceneName;
                Time.timeScale = 0f;
            }
            else if (classSelector != null)
            {
                pauseManager.canPause = true;
                Time.timeScale = 1f;
                classSelector.gameObject.SetActive(false);
                interaccion.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && portalUnlocked)
        {
            if (interaccion != null)
            {
                interaccion.SetActive(true);
            }
            inPortalZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inPortalZone = false;
            isInteracting = false;  // Reset the interaction state
            if (interaccion != null)
            {
                interaccion.SetActive(false);
            }
        }
    }
}