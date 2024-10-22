using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using System;


public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private Coroutine typingCoroutine;
    private string[] dialogueLines;
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool lineFullyDisplayed = false;
    private bool isMoloDialogue; // Indica si es diálogo con Molo

    // Referencia a WaveManager
    private WaveManager waveManager;

    private void Start()
    {
        // Buscar WaveManager en la escena
        waveManager = WaveManager.Instance;
    }

    // Método para iniciar el diálogo, incluyendo si es diálogo con el jefe Molo
    public void StartDialogue(string text, bool isMolo)
    {
        dialogueLines = text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); // Divide el texto en líneas
        currentLineIndex = 0;
        isMoloDialogue = isMolo; // Marca si es diálogo de Molo
        ShowNextLine();
    }

    // Muestra la siguiente línea de texto
    private void ShowNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));
        }
        else
        {
            // Si se completan todas las líneas, limpia el texto y verifica el diálogo con Molo
            dialogueText.text = "";

            if (isMoloDialogue)
            {
                // Si el diálogo es con Molo, actualizar EndDialogueBos en WaveManager
                waveManager.EndDialogueBos = true;
            }
        }
    }

    // Corrutina para escribir el texto letra por letra
    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        lineFullyDisplayed = false;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Controla la velocidad del texto
        }
        lineFullyDisplayed = true;
        isTyping = false;
    }

    // Método para completar inmediatamente la línea actual
    private void CompleteLine()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueLines[currentLineIndex]; // Completa la línea de inmediato
            isTyping = false;
            lineFullyDisplayed = true;
        }
    }

    public void ClearText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Detiene la escritura progresiva si está en curso
        }
        dialogueText.text = ""; // Vacía el texto en pantalla
        isTyping = false; // Asegura que el sistema sepa que ya no se está escribiendo
        lineFullyDisplayed = false; // Resetea el estado de la línea actual
    }

    public void ResetDialogue()
    {
        // Detiene cualquier corrutina de escritura en curso
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        // Restablece todas las variables internas del diálogo
        dialogueText.text = "";
        currentLineIndex = 0;
        isTyping = false;
        lineFullyDisplayed = false;
        dialogueLines = null;
    }

    // Método para avanzar al siguiente renglón cuando el actual está completo
    public void OnSpacePressed()
    {
        if (!lineFullyDisplayed)
        {
            CompleteLine(); // Completa la línea si aún se está escribiendo
        }
        else
        {
            currentLineIndex++;
            ShowNextLine(); // Pasa a la siguiente línea
        }
    }
}