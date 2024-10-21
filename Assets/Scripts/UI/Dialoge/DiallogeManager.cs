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

    private bool isMoloDialogue = false; // Nuevo campo para saber si es el diálogo de Molo

    // Evento que se dispara al finalizar el diálogo
    public event Action OnDialogueEnd;

    // Método para iniciar el diálogo, pasando las líneas del texto y si es el jefe final
    public void StartDialogue(string text, bool isMolo)
    {
        dialogueLines = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries); // Divide el texto en líneas
        currentLineIndex = 0;
        isMoloDialogue = isMolo; // Se asigna si es diálogo de Molo
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
            dialogueText.text = "";
            OnDialogueEnd?.Invoke(); // Dispara el evento cuando se completan todas las líneas

            if (isMoloDialogue)
            {
                WaveManager.Instance.EndDialogueBos = true; // Activa el bool solo si es el jefe final
                Debug.Log("El diálogo con Molo ha terminado. EndDialogueBos = true");
            }
        }
    }

    // Método para gestionar cuando se presiona la barra espaciadora
    public void OnSpacePressed()
    {
        if (isTyping && !lineFullyDisplayed)
        {
            // Completa la línea actual inmediatamente
            CompleteCurrentLine();
        }
        else if (!isTyping)
        {
            // Muestra la siguiente línea
            currentLineIndex++;
            ShowNextLine();
        }
    }

    // Método para limpiar el texto
    public void ClearText()
    {
        dialogueText.text = "";
    }

    // Método para completar la línea actual de inmediato
    private void CompleteCurrentLine()
    {
        StopCoroutine(typingCoroutine);
        dialogueText.text = dialogueLines[currentLineIndex];
        isTyping = false;
        lineFullyDisplayed = true;
    }

    // Corrutina para mostrar el texto progresivamente
    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Controla la velocidad de tipeo
        }
        isTyping = false;
        lineFullyDisplayed = true;
    }

}