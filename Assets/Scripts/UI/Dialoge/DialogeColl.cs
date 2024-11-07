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
     
    public GameObject Dialoge;

    private string dialogueText;

    public bool isMoloDialogue = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager == null)
        {
            Debug.LogError("DialogueManager no encontrado en la escena.");
        }

        LoadDialogueFromFile();
        if (pressTObject != null)
        {
            pressTObject.SetActive(false);
        }

        
        Dialoge = GameObject.Find("Dialoge");

        if (Dialoge != null)
        {
            
            Dialoge.SetActive(false);
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
                dialogueManager.ClearText(); 
                dialogueManager.ResetDialogue(); 
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {


            if (pressTObject != null)
            {
                pressTObject.SetActive(false);
                
            }
            if (dialogueManager != null)
            {
                // Verificación si el diálogo está en curso
                if (dialogueManager.IsDialogueActive)
                {
                    
                    
                        
                    

                    dialogueManager.OnSpacePressed(); 
                }
                else
                {
                    
                    if (Dialoge.activeSelf == true)
                    {
                        Dialoge.SetActive(false);
                    }

                    dialogueManager.StartDialogue(dialogueText, isMoloDialogue); 
                }
            }
        }
    }
}