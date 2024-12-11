using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogeColl : MonoBehaviour
{
    public TextAsset textFile;
    public GameObject pressTObject;
    private bool playerInRange = false;
    private DialogueManager dialogueManager;
    private string dialogueText;
    private PauseManager pauseManager;

    public bool isMoloDialogue = false;

    [SerializeField] public WeaponManager weaponManager;

    public enum TutorialWeapon
    {
        Melee,
        Engineer,
        Ranged,
    }
    [SerializeField] private TutorialWeapon selectedWeapon;
    private IWeapon currentClassWeapon;


    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager no encontrado en la escena.");
            return;
        }

        LoadDialogueFromFile();

        if (pressTObject != null)
        {
            pressTObject.SetActive(false);
        }
    }

    private void LoadDialogueFromFile()
    {
        if (textFile != null)
        {
            dialogueText = textFile.text;
        }
        else
        {
            Debug.LogError("Archivo de diálogo no asignado en el Inspector.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (pressTObject != null)
            {
                pressTObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressTObject != null)
            {
                pressTObject.SetActive(false);
            }

            if (dialogueManager != null)
            {
                dialogueManager.DeactivateDialogue();
                dialogueManager.ClearText();
                dialogueManager.ResetDialogue();
            }
        }
    }

    private void Update()
    {

        if (pauseManager != null && pauseManager.gamePaused)
        {
            return;
        }


        if (dialogueManager != null && dialogueManager.ultimoDialogo)
        {
            return;
        }

        HandleEscapeKey();

        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            pauseManager.canPause = false;

            if (pressTObject != null)
            {
                pressTObject.SetActive(false);
            }

            if (dialogueManager != null)
            {
                if (dialogueManager.IsDialogueActive)
                {
                    dialogueManager.OnSpacePressed();

                }
                else
                {

                    dialogueManager.StartDialogue(dialogueText, isMoloDialogue);
                }

            }

            ActivateWeaponClass();

        }



        if (dialogueManager.DialogeActive == false && playerInRange)
        {
            StartCoroutine(ResetEscapeFlag());
            pressTObject.SetActive(true);
        }
    }

    private void HandleEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (dialogueManager != null && dialogueManager.IsDialogueActive)
            {
                dialogueManager.DeactivateDialogue();
                dialogueManager.ClearText();
                dialogueManager.ResetDialogue();

            }
        }
    }

    private IEnumerator ResetEscapeFlag()
    {

        yield return null;

        pauseManager.canPause = true;
    }

    private void ActivateWeaponClass()
    {
        if(weaponManager != null)
            weaponManager.ClearAutomaticWeapon();
        switch (selectedWeapon)
        {
            case TutorialWeapon.Melee:
                weaponManager._meleeCanAttack = true;
                currentClassWeapon = FindAnyObjectByType<SwordAttack>();
                weaponManager.AddAutomaticWeapon(currentClassWeapon);
                break;
            case TutorialWeapon.Ranged:
                weaponManager._rangedCanAttack = true;
                currentClassWeapon = FindAnyObjectByType<RangedAttack>();
                weaponManager.AddAutomaticWeapon(currentClassWeapon);
                break;
            case TutorialWeapon.Engineer:
                weaponManager._engineerCanAttack = true;
                currentClassWeapon = FindAnyObjectByType<OrbeAttack>();
                weaponManager.AddAutomaticWeapon(currentClassWeapon);
                break;
        }
    }

}