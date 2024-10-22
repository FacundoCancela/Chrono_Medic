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

    // Nuevo campo para identificar si el diálogo es con el jefe final
    public bool EndDialogueBos = false;

    // Método para iniciar el diálogo, pasando las líneas del texto
    public void StartDialogue(string text, bool isFinalBossDialogue)
    {
        dialogueLines = text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); // Divide el texto en líneas
        currentLineIndex = 0;
        ShowNextLine();

        // Si es diálogo con el jefe final, no se establece el booleano hasta que termine el diálogo
        if (isFinalBossDialogue)
        {
            EndDialogueBos = false; // Asegura que comience como false
        }
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
            // Si se completan todas las líneas, limpia el texto y actualiza el estado
            dialogueText.text = "";
            if (EndDialogueBos)
            {
                EndDialogueBos = true; // Establece el booleano si se ha terminado el diálogo del jefe final
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

    // Método para reiniciar el diálogo
    public void ResetDialogue()
    {
        currentLineIndex = 0; // Reinicia el índice
        ClearText(); // Limpia el texto actual
        EndDialogueBos = false; // Resetea el booleano al salir
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