using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Campo de texto para mostrar el diálogo.
    public TextMeshProUGUI characterNameText; // Campo de texto para mostrar el nombre del personaje.
    public float typingSpeed = 0.05f; // Velocidad de escritura de cada letra.
    public int dialogueOrder = 1; // Orden del diálogo en la escena (configurable en el inspector)

    private Queue<string> sentences; // Cola para almacenar las frases de diálogo.
    private bool isTyping; // Controla si se está escribiendo una frase actualmente.

    void Start()
    {
        sentences = new Queue<string>(); // Inicializa la cola de frases.

        LoadDialogue(); // Carga el diálogo inicial basado en la escena y el orden.
    }

    void LoadDialogue()
    {
        string sceneName = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual.
        string dialogueFileName = sceneName + "_" + dialogueOrder.ToString(); // Construye el nombre del archivo de texto.

        LoadDialogueFromFile(dialogueFileName); // Carga el diálogo desde el archivo de texto correspondiente.
    }

    void LoadDialogueFromFile(string fileName)
    {
        // Carga el archivo de texto desde la carpeta Resources.
        TextAsset dialogueFile = Resources.Load<TextAsset>(fileName);

        if (dialogueFile != null)
        {
            sentences.Clear(); // Limpia las frases previas antes de cargar nuevas.
            string[] dialogueLines = dialogueFile.text.Split('\n'); // Divide el texto en líneas.

            foreach (string line in dialogueLines)
            {
                sentences.Enqueue(line.Trim()); // Añade cada línea a la cola.
            }

            DisplayNextSentence(); // Muestra la primera frase del nuevo diálogo.
        }
        else
        {
            Debug.LogError("No se encontró el archivo de diálogo: " + fileName);
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
        ParseSentence(sentence); // Analiza y muestra la frase con el nombre del personaje.
    }

    void ParseSentence(string sentence)
    {
        // Separa el nombre del personaje del texto del diálogo usando ":" como delimitador.
        if (sentence.Contains(":"))
        {
            string[] parts = sentence.Split(new char[] { ':' }, 2);
            string characterName = parts[0].Trim();
            string dialogue = parts[1].Trim();

            characterNameText.text = characterName; // Muestra el nombre del personaje.
            StartCoroutine(TypeSentence(dialogue)); // Comienza a escribir el texto del diálogo.
        }
        else
        {
            // Si no hay un delimitador, se asume que es solo una línea de diálogo sin nombre.
            characterNameText.text = ""; // No muestra ningún nombre de personaje.
            StartCoroutine(TypeSentence(sentence)); // Comienza a escribir el texto del diálogo.
        }
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
        if (Input.GetKeyDown(KeyCode.K)) // Avanza el diálogo al presionar la tecla Espacio.
        {
            SaltaDialogo();
        }
    }

    public void SaltaDialogo()
    {
        dialogueOrder++; // Incrementa el número de orden del diálogo.
        LoadDialogue(); // Carga el siguiente diálogo basado en el nuevo número de orden.
    }
}