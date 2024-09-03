using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Campo de texto para mostrar el di�logo.
    public TextMeshProUGUI characterNameText; // Campo de texto para mostrar el nombre del personaje.
    public float typingSpeed = 0.05f; // Velocidad de escritura de cada letra.
    public int dialogueOrder = 1; // Orden del di�logo en la escena (configurable en el inspector)

    private Queue<string> sentences; // Cola para almacenar las frases de di�logo.
    private bool isTyping; // Controla si se est� escribiendo una frase actualmente.

    void Start()
    {
        sentences = new Queue<string>(); // Inicializa la cola de frases.

        LoadDialogue(); // Carga el di�logo inicial basado en la escena y el orden.
    }

    void LoadDialogue()
    {
        string sceneName = SceneManager.GetActiveScene().name; // Obtiene el nombre de la escena actual.
        string dialogueFileName = sceneName + "_" + dialogueOrder.ToString(); // Construye el nombre del archivo de texto.

        LoadDialogueFromFile(dialogueFileName); // Carga el di�logo desde el archivo de texto correspondiente.
    }

    void LoadDialogueFromFile(string fileName)
    {
        // Carga el archivo de texto desde la carpeta Resources.
        TextAsset dialogueFile = Resources.Load<TextAsset>(fileName);

        if (dialogueFile != null)
        {
            sentences.Clear(); // Limpia las frases previas antes de cargar nuevas.
            string[] dialogueLines = dialogueFile.text.Split('\n'); // Divide el texto en l�neas.

            foreach (string line in dialogueLines)
            {
                sentences.Enqueue(line.Trim()); // A�ade cada l�nea a la cola.
            }

            DisplayNextSentence(); // Muestra la primera frase del nuevo di�logo.
        }
        else
        {
            Debug.LogError("No se encontr� el archivo de di�logo: " + fileName);
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
        ParseSentence(sentence); // Analiza y muestra la frase con el nombre del personaje.
    }

    void ParseSentence(string sentence)
    {
        // Separa el nombre del personaje del texto del di�logo usando ":" como delimitador.
        if (sentence.Contains(":"))
        {
            string[] parts = sentence.Split(new char[] { ':' }, 2);
            string characterName = parts[0].Trim();
            string dialogue = parts[1].Trim();

            characterNameText.text = characterName; // Muestra el nombre del personaje.
            StartCoroutine(TypeSentence(dialogue)); // Comienza a escribir el texto del di�logo.
        }
        else
        {
            // Si no hay un delimitador, se asume que es solo una l�nea de di�logo sin nombre.
            characterNameText.text = ""; // No muestra ning�n nombre de personaje.
            StartCoroutine(TypeSentence(sentence)); // Comienza a escribir el texto del di�logo.
        }
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
        if (Input.GetKeyDown(KeyCode.K)) // Avanza el di�logo al presionar la tecla Espacio.
        {
            SaltaDialogo();
        }
    }

    public void SaltaDialogo()
    {
        dialogueOrder++; // Incrementa el n�mero de orden del di�logo.
        LoadDialogue(); // Carga el siguiente di�logo basado en el nuevo n�mero de orden.
    }
}