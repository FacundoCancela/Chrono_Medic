using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    private Coroutine typingCoroutine;
    private string[] dialogueLines;
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool lineFullyDisplayed = false;

    // M�todo para iniciar el di�logo, pasando las l�neas del texto
    public void StartDialogue(string text)
    {
        dialogueLines = text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); // Divide el texto en l�neas
        currentLineIndex = 0;
        ShowNextLine();
    }

    // Muestra la siguiente l�nea de texto
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
            // Si se completan todas las l�neas, limpia el texto
            dialogueText.text = "";
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

    // M�todo para completar inmediatamente la l�nea actual
    private void CompleteLine()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueLines[currentLineIndex]; // Completa la l�nea de inmediato
            isTyping = false;
            lineFullyDisplayed = true;
        }
    }

    public void ClearText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Detiene la escritura progresiva si est� en curso
        }
        dialogueText.text = ""; // Vac�a el texto en pantalla
        isTyping = false; // Asegura que el sistema sepa que ya no se est� escribiendo
        lineFullyDisplayed = false; // Resetea el estado de la l�nea actual
    }

    // M�todo para avanzar al siguiente rengl�n cuando el actual est� completo
    public void OnSpacePressed()
    {
        if (!lineFullyDisplayed)
        {
            CompleteLine(); // Completa la l�nea si a�n se est� escribiendo
        }
        else
        {
            currentLineIndex++;
            ShowNextLine(); // Pasa a la siguiente l�nea
        }
    }
}