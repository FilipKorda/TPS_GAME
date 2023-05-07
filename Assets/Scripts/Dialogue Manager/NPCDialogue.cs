using System;
using System.Reflection;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public Dialogue dialogue; 
    public DialogueManager dialogueManager;
    public GameObject dialogueBox;

    private int index = 0;

    public void StartDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
        Time.timeScale = 0f;

        dialogueBox.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (index < dialogue.sentences.Length)
        {
            dialogueManager.dialogueText.text = dialogue.sentences[index];
            index++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        Time.timeScale = 1f;
        dialogueBox.SetActive(false);
        index = 0;
    }

}