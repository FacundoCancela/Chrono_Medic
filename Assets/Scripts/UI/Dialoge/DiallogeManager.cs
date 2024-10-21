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

    private bool isMoloDialogue = false; // Nuevo campo para saber si es el di�logo de Molo

    // Evento que se dispara al finalizar el di�logo
    public event Action OnDialogueEnd;

    // M�todo para iniciar el di�logo, pasando las l�neas del texto y si es el jefe final
    public void StartDialogue(string text, bool isMolo)
    {
        dialogueLines = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries); // Divide el texto en l�neas
        currentLineIndex = 0;
        isMoloDialogue = isMolo; // Se asigna si es di�logo de Molo
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
            dialogueText.text = "";
            OnDialogueEnd?.Invoke(); // Dispara el evento cuando se completan todas las l�neas

            if (isMoloDialogue)
            {
                WaveManager.Instance.EndDialogueBos = true; // Activa el bool solo si es el jefe final
                Debug.Log("El di�logo con Molo ha terminado. EndDialogueBos = true");
            }
        }
    }

    // M�todo para gestionar cuando se presiona la barra espaciadora
    public void OnSpacePressed()
    {
        if (isTyping && !lineFullyDisplayed)
        {
            // Completa la l�nea actual inmediatamente
            CompleteCurrentLine();
        }
        else if (!isTyping)
        {
            // Muestra la siguiente l�nea
            currentLineIndex++;
            ShowNextLine();
        }
    }

    // M�todo para limpiar el texto
    public void ClearText()
    {
        dialogueText.text = "";
    }

    // M�todo para completar la l�nea actual de inmediato
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