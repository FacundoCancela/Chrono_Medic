using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogeColl : MonoBehaviour
{
    public TextAsset textFile; // Asigna el archivo .txt desde el inspector
    public TextMeshProUGUI pressTText; // Referencia al TextMeshPro para la indicación de "Presiona T"
    private bool playerInRange = false;
    private DialogueManager dialogueManager;
    private string dialogueText;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>(); // Busca el DialogueManager en la escena
        LoadDialogueFromFile();
        pressTText.gameObject.SetActive(false); // Oculta el mensaje al inicio
    }

    // Método para cargar el texto desde el TextAsset
    private void LoadDialogueFromFile()
    {
        if (textFile != null)
        {
            dialogueText = textFile.text; // Lee el contenido del archivo .txt
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
            playerInRange = true; // Detecta si el jugador está en el rango
            pressTText.gameObject.SetActive(true); // Muestra el mensaje "Presiona T"
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Sale del rango
            pressTText.gameObject.SetActive(false); // Oculta el mensaje "Presiona T"
            dialogueManager.ClearText(); // Limpia el texto en pantalla y detiene la escritura progresiva
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.T)) // Detecta la tecla "T"
        {
            pressTText.gameObject.SetActive(false); // Oculta el mensaje "Presiona T" cuando se presiona T
            dialogueManager.StartDialogue(dialogueText); // Comienza el diálogo
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.Space)) // Detecta la barra espaciadora
        {
            dialogueManager.OnSpacePressed(); // Completa o avanza el diálogo
        }
    }
}