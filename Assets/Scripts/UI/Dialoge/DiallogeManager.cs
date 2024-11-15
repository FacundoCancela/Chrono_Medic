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
    public GameObject Dialoge; 
    private Coroutine typingCoroutine;
    private string[] dialogueLines;
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool lineFullyDisplayed = false;
    private bool isMoloDialogue;

    public event System.Action OnDialogueEnd;

    // Referencia a WaveManager
    private WaveManager waveManager;

    private void Start()
    {
        waveManager = WaveManager.Instance;
        if (Dialoge != null)
        {
            Dialoge.SetActive(false); 
        }
    }

    public bool IsDialogueActive
    {
        get
        {
            return dialogueLines != null && currentLineIndex < dialogueLines.Length;
        }
    }

    public void ActivateDialogue()
    {
        if (Dialoge != null)
        {
            Dialoge.SetActive(true);
        }
    }

    public void DeactivateDialogue()
    {
        if (Dialoge != null)
        {
            Dialoge.SetActive(false);
        }
    }

    public void StartDialogue(string text, bool isMolo)
    {
        dialogueLines = text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        currentLineIndex = 0;
        isMoloDialogue = isMolo;
        ActivateDialogue(); 
        ShowNextLine();
    }

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
            if (isMoloDialogue)
            {
                waveManager.EndDialogueBos = true;
            }
            OnDialogueEnd?.Invoke();
            DeactivateDialogue(); 
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        lineFullyDisplayed = false;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        lineFullyDisplayed = true;
        isTyping = false;
    }

    private void CompleteLine()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueLines[currentLineIndex];
            isTyping = false;
            lineFullyDisplayed = true;
        }
    }

    public void ClearText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        dialogueText.text = "";
        isTyping = false;
        lineFullyDisplayed = false;
    }

    public void ResetDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        dialogueText.text = "";
        currentLineIndex = 0;
        isTyping = false;
        lineFullyDisplayed = false;
        dialogueLines = null;
    }

    public void OnSpacePressed()
    {
        if (!lineFullyDisplayed)
        {
            CompleteLine();
        }
        else
        {
            currentLineIndex++;
            ShowNextLine();
        }
    }
}