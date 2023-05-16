using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("==== Conversation ====")]
    [Space(10)]
    public List<Dialogue> conversation = new List<Dialogue>();    
    [Header("==== Others ====")]
    [Space(10)]
    public DialogueManager dialogueManager;
    public InteractWithNPC interactWithNPC;
    public GameObject dialogueBox;
    private Dialogue dialogue;
    private int index = 0;
    private bool dialogueCompleted = false;

    public void StartDialogue()
    {
        if (!dialogueCompleted)
        {
            SetDialogue(conversation[0]);
            dialogueManager.StartDialogue(dialogue);
            Time.timeScale = 0f;
            dialogueBox.SetActive(true);
            DisplayNextSentence();
        }
    }
    public void DisplayNextSentence()
    {
        if (index < conversation.Count)
        {
            dialogue = conversation[index];
            dialogueManager.characterNameText.text = dialogue.name;
            dialogueManager.portraitImage.sprite = dialogue.portrait;
            dialogueManager.dialogueText.text = dialogue.sentences;
            index++;
        }
        else
        {
            EndDialogue();
        }
    }
    public void SetDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
    }
    public void EndDialogue()
    {
        Time.timeScale = 1f;
        dialogueBox.SetActive(false);
        index = 0;
        dialogueCompleted = true;
        Debug.Log("Koniec Dialogu");
    }
}