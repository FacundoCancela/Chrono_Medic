using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Campo de texto para mostrar el di�logo.
    public float typingSpeed = 0.05f; // Velocidad de escritura de cada letra.

    private Queue<string> sentences; // Cola para almacenar las frases de di�logo.
    private bool isTyping; // Controla si se est� escribiendo una frase actualmente.

    void Start()
    {
        sentences = new Queue<string>(); // Inicializa la cola de frases.

        LoadDialogueFromFile("dialogue"); // Carga el di�logo desde un archivo de texto.
    }

    void LoadDialogueFromFile(string fileName)
    {
        // Carga el archivo de texto desde la carpeta Resources.
        TextAsset dialogueFile = Resources.Load<TextAsset>(fileName);

        if (dialogueFile != null)
        {
            string[] dialogueLines = dialogueFile.text.Split('\n'); // Divide el texto en l�neas.

            foreach (string line in dialogueLines)
            {
                sentences.Enqueue(line.Trim()); // A�ade cada l�nea a la cola.
            }

            DisplayNextSentence(); // Muestra la primera frase.
        }
        else
        {
            Debug.LogError("No se encontr� el archivo de di�logo en Resources.");
        }
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines(); // Detiene la escritura si el jugador avanza r�pidamente.
            dialogueText.text = sentences.Peek(); // Completa la frase actual.
            isTyping = false;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue(); // Termina el di�logo si no hay m�s frases.
            return;
        }

        string sentence = sentences.Dequeue(); // Toma la siguiente frase de la cola.
        StartCoroutine(TypeSentence(sentence)); // Comienza la escritura.
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; // Indica que se est� escribiendo una frase.
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
        Debug.Log("End of dialogue."); // Muestra un mensaje de fin de di�logo.
        // Aqu� puedes a�adir la l�gica para cerrar la ventana de di�logo o continuar con el juego.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Avanza el di�logo al presionar la tecla Espacio.
        {
            DisplayNextSentence();
        }
    }
}