using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI characterNameText; 
    public float typingSpeed = 0.05f; 
    public int dialogueOrder = 1; 

    public Dictionary<string, GameObject> characterImages; 

    private Queue<string> sentences; 
    private bool isTyping; 

    void Start()
    {
        sentences = new Queue<string>();
        characterImages = new Dictionary<string, GameObject>(); 

     
        //LoadCharacterImages();

        //LoadDialogue();
    }

    void LoadCharacterImages()
    {

        characterImages.Clear(); 

        
        foreach (Transform child in transform)
        {
            if (child.CompareTag("CharacterImage")) 
            {
                if (!characterImages.ContainsKey(child.name)) 
                {
                    characterImages.Add(child.name, child.gameObject);
                    child.gameObject.SetActive(false); 
                }
                else
                {
                    Debug.LogWarning("El personaje " + child.name + " ya existe en el diccionario.");
                }
            }
        }
    }

    void LoadDialogue()
    {
        string sceneName = SceneManager.GetActiveScene().name; 
        string dialogueFileName = sceneName + "_" + dialogueOrder.ToString(); 

        LoadDialogueFromFile(dialogueFileName); 
    }

    void LoadDialogueFromFile(string fileName)
    {
        
        TextAsset dialogueFile = Resources.Load<TextAsset>(fileName);

        if (dialogueFile != null)
        {
            sentences.Clear(); 
            string[] dialogueLines = dialogueFile.text.Split('\n'); 

            foreach (string line in dialogueLines)
            {
                sentences.Enqueue(line.Trim());
            }

            DisplayNextSentence();
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
            StopAllCoroutines(); 

            if (sentences.Count > 0)
            {
                dialogueText.text = sentences.Peek(); 
            }
            else
            {
                dialogueText.text = ""; 
            }

            isTyping = false;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue(); 
            return;
        }

        string sentence = sentences.Dequeue(); 
        ParseSentence(sentence); 
    }

    void ParseSentence(string sentence)
    {
        
        if (sentence.Contains(":"))
        {
            string[] parts = sentence.Split(new char[] { ':' }, 2);
            string characterName = parts[0].Trim();
            string dialogue = parts[1].Trim();

            characterNameText.text = characterName; 
            ActivateCharacterImage(characterName); 
            StartCoroutine(TypeSentence(dialogue)); 
        }
        else
        {
            
            characterNameText.text = ""; 
            DeactivateAllCharacterImages(); 
            StartCoroutine(TypeSentence(sentence)); 
        }
    }

    void ActivateCharacterImage(string characterName)
    {
        DeactivateAllCharacterImages(); 

        if (characterImages.ContainsKey(characterName))
        {
            characterImages[characterName].SetActive(true);
        }
        else
        {
            Debug.LogWarning("No se encontró la imagen para el personaje: " + characterName);
        }
    }

    void DeactivateAllCharacterImages()
    {
        foreach (KeyValuePair<string, GameObject> entry in characterImages)
        {
            entry.Value.SetActive(false); 
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; 
        dialogueText.text = ""; 

        foreach (char letter in sentence)
        {
            dialogueText.text += letter; 
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false; 
    }

    void EndDialogue()
    {
        Debug.Log("End of dialogue."); 
        DeactivateAllCharacterImages();
        characterNameText.text = "";
        dialogueText.text = "";

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            DisplayNextSentence();
        }
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            SaltaDialogo();
        }
    }

    public void SaltaDialogo()
    {
        dialogueOrder++; 
        LoadCharacterImages();
        LoadDialogue(); 
    }
}