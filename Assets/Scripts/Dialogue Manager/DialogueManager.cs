using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;
    private Queue<string> sentenceQueue;
    private bool isDisplayingSentence = false;
    public void StartDialogue(Dialogue dialogue)
    {
        characterNameText.text = dialogue.name;
        portraitImage.sprite = dialogue.portrait;
        sentenceQueue = new Queue<string>();                      
        DisplayNextSentence();
    }

    public void StartLastDialogue(LastDialogue lastDialogue)
    {
        characterNameText.text = lastDialogue.lastName;
        portraitImage.sprite = lastDialogue.lastPortrait;
        sentenceQueue = new Queue<string>();
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isDisplayingSentence)
        {
            return;
        }

        if (sentenceQueue.Count == 0)
        {
            NextDialogue();
            return;
        }

        string sentence = sentenceQueue.Dequeue();

    }
    
    void NextDialogue()
    {
        Debug.Log("Nastepny dialog");
    }
}
