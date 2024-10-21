using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogeColl : MonoBehaviour
{
    public TextAsset textFile; // Asigna el archivo .txt desde el inspector
    public GameObject pressTObject; // Referencia al GameObject que indica "Presiona T"
    private bool playerInRange = false;
    private DialogueManager dialogueManager;
    private string dialogueText;

    // Nuevo campo para identificar si el diálogo es con el jefe final
    public bool isMoloDialogue = false;

    private void Start()
    {
        // Buscar el GameObject que contiene el texto "Indicate" directamente
        pressTObject = GameObject.Find("Interaccion"); // Busca el GameObject por su nombre

        if (pressTObject == null)
        {
            Debug.LogError("No se encontró el GameObject 'Interaccion' en la escena.");
        }

        dialogueManager = FindObjectOfType<DialogueManager>(); // Busca el DialogueManager en la escena
        LoadDialogueFromFile();
        pressTObject.SetActive(false); // Oculta el mensaje al inicio
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
            pressTObject.SetActive(true); // Muestra el GameObject "Indicate"
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Sale del rango
            pressTObject.SetActive(false); // Oculta el GameObject "Indicate"
            dialogueManager.ClearText(); // Limpia el texto en pantalla y detiene la escritura progresiva
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F)) // Detecta la tecla "F"
        {
            pressTObject.SetActive(false); // Oculta el GameObject "Indicate" cuando se presiona F
            dialogueManager.StartDialogue(dialogueText, isMoloDialogue); // Pasa el bool que indica si es diálogo de Molo
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.Space)) // Detecta la barra espaciadora
        {
            dialogueManager.OnSpacePressed(); // Completa o avanza el diálogo
        }
    }
}