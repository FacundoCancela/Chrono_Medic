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

    Animator animator;

    public AudioClip clip;
    public AudioSource source;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        if (portalUnlocked)
        {
            spriteRenderer.sprite = unlockedPortal;
            animator.SetTrigger("Idle");
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
            if(classSelector != null)
            {
                if (source != null && clip !=null)
                {
                    source.PlayOneShot(clip);
                }
                classSelector.gameObject.SetActive(true);
                classSelector.sceneName = sceneName;
            }            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(classSelector != null)
            {
                classSelector.gameObject.SetActive(false);
            }
        }
    }
}
