using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicQuestDialogue : MonoBehaviour
{
    [Header("==== Conversation ====")]
    [Space(10)]
    public List<Dialogue> conversation = new();
    [Header("==== Conversation ====")]
    [Space(10)]
    public List<LastDialogue> lastConversation = new();
    [Header("==== Others ====")]
    [Space(10)]
    public DialogueManager dialogueManager;
    public InteractWithNPC interactWithNPC;
    public GameObject dialogueBox;
    private Dialogue dialogue;
    private LastDialogue lastDialogue;
    private int index = 0;
    public bool dialogueQuestCompleted = false;
    public bool lastDialogueAvailable = false;

    public void StartDialogue()
    {
        if (!dialogueQuestCompleted)
        {
            interactWithNPC.isInteracting = true;
            SetDialogue(conversation[0]);
            dialogueManager.StartDialogue(dialogue);
            Time.timeScale = 0f;
            dialogueBox.SetActive(true);
            DisplayNextSentence();
        }

        if (dialogueQuestCompleted)
        {
            interactWithNPC.isInteracting = true;
            SetLastDialogue(lastConversation[0]);
            dialogueManager.StartLastDialogue(lastDialogue);
            Time.timeScale = 0f;
            dialogueBox.SetActive(true);
            DisplayLastSentance();
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
    public void SetLastDialogue(LastDialogue lastDialogue)
    {
        this.lastDialogue = lastDialogue;
    }
    public void EndDialogue()
    {
        Time.timeScale = 1f;
        dialogueBox.SetActive(false);
        index = 0;
        dialogueQuestCompleted = true;
        lastDialogueAvailable = true;
        interactWithNPC.isInteracting = false;
        Debug.Log("Koniec Dialogu");
    }

    public void DisplayLastSentance()
    {

        if (index < lastConversation.Count)
        {
            lastDialogue = lastConversation[index];
            dialogueManager.characterNameText.text = lastDialogue.lastName;
            dialogueManager.portraitImage.sprite = lastDialogue.lastPortrait;
            dialogueManager.dialogueText.text = lastDialogue.lastSentences;
            index++;
        }
        else
        {
            EndDialogue();
        }
    }
}
