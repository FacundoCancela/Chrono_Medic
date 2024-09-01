using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Campo de texto para mostrar el diálogo.
    public float typingSpeed = 0.05f; // Velocidad de escritura de cada letra.

    private Queue<string> sentences; // Cola para almacenar las frases de diálogo.
    private bool isTyping; // Controla si se está escribiendo una frase actualmente.

    void Start()
    {
        sentences = new Queue<string>(); // Inicializa la cola de frases.

        LoadDialogueFromFile("dialogue"); // Carga el diálogo desde un archivo de texto.
    }

    void LoadDialogueFromFile(string fileName)
    {
        // Carga el archivo de texto desde la carpeta Resources.
        TextAsset dialogueFile = Resources.Load<TextAsset>(fileName);

        if (dialogueFile != null)
        {
            string[] dialogueLines = dialogueFile.text.Split('\n'); // Divide el texto en líneas.

            foreach (string line in dialogueLines)
            {
                sentences.Enqueue(line.Trim()); // Añade cada línea a la cola.
            }

            DisplayNextSentence(); // Muestra la primera frase.
        }
        else
        {
            Debug.LogError("No se encontró el archivo de diálogo en Resources.");
        }
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines(); // Detiene la escritura si el jugador avanza rápidamente.
            dialogueText.text = sentences.Peek(); // Completa la frase actual.
            isTyping = false;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue(); // Termina el diálogo si no hay más frases.
            return;
        }

        string sentence = sentences.Dequeue(); // Toma la siguiente frase de la cola.
        StartCoroutine(TypeSentence(sentence)); // Comienza la escritura.
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; // Indica que se está escribiendo una frase.
        dialogueText.text = ""; // Limpia el texto actual.

        foreach (char letter in sentence)
        {
            dialogueText.text += letter; // Agrega cada letra con un retraso.
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false; // Indica que ha terminado la escritura.
    }

    void EndDialogue()
    {
        Debug.Log("End of dialogue."); // Muestra un mensaje de fin de diálogo.
        // Aquí puedes añadir la lógica para cerrar la ventana de diálogo o continuar con el juego.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Avanza el diálogo al presionar la tecla Espacio.
        {
            DisplayNextSentence();
        }
    }
}