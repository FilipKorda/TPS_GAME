using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("==== Conversation ====")]
    [Space(10)]
    public List<Dialogue> conversation = new List<Dialogue>();

    [Header("==== Last Conversation ====")]
    [Space(10)]
    public List<LastDialogue> lastConversation = new List<LastDialogue>();

    [Header("==== Others ====")]
    [Space(10)]
    public DialogueManager dialogueManager;
    public InteractWithNPC interactWithNPC;
    public GameObject dialogueBox;

    private Dialogue dialogue;
    private LastDialogue lastDialogue;
    private int index = 0;
    public bool dialogueCompleted = false;
    public bool lastDialogueAvailable = false;

    [Header("==== Quests ====")]
    [Space(10)]
    public MechanicGuestList mechanicGuests;

    public void StartDialogue()
    {
        if (!dialogueCompleted)
        {
            interactWithNPC.isInteracting = true;
            SetDialogue(conversation[0]);
            dialogueManager.StartDialogue(dialogue);
            Time.timeScale = 0f;
            dialogueBox.SetActive(true);
            DisplayNextSentence();
        }

        if (dialogueCompleted)
        {
            interactWithNPC.isInteracting = true;
            SetLastDialogue(lastConversation[0]);
            dialogueManager.StartLastDialogue(lastDialogue);
            Time.timeScale = 0f;
            dialogueBox.SetActive(true);
            DisplayLastSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (index < conversation.Count)
        {
            dialogue = conversation[index];
            dialogueManager.characterNameText.text = dialogue.name;
            dialogueManager.portraitImage.sprite = dialogue.portrait;
            dialogueManager.dialogueText.text = string.Join("\n", dialogue.sentences);

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
        dialogueCompleted = true;
        lastDialogueAvailable = true;
        interactWithNPC.isInteracting = false;
        MechanicQuestComplete();
    }

    public void DisplayLastSentence()
    {
        if (index < lastConversation.Count)
        {
            lastDialogue = lastConversation[index];
            dialogueManager.characterNameText.text = lastDialogue.lastName;
            dialogueManager.portraitImage.sprite = lastDialogue.lastPortrait;
            dialogueManager.dialogueText.text = string.Join("\n", lastDialogue.lastSentences);

            index++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void MechanicQuestComplete()
    {
        if (dialogueCompleted)
        {
            if (mechanicGuests.hammer != null && mechanicGuests.crossbar != null && mechanicGuests.chisel != null)
            {
                mechanicGuests.hammer.SetActive(true);
                mechanicGuests.crossbar.SetActive(true);
                mechanicGuests.chisel.SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class MechanicGuestList
{
    public GameObject hammer;
    public GameObject crossbar;
    public GameObject chisel;
}


