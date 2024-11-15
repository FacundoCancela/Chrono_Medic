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
            return; // Salir del Update si el juego está pausado
        }




        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
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
        }
    }
}