using System;
using System.Reflection;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    [Header("==== Others ====")]
    [Space(10)]
    public DialogueManager dialogueManager;
    public InteractWithNPC interactWithNPC;
    public GameObject dialogueBox;

    public enum Speaker { First, Second };
    private int index1 = 0;
    private int index2 = 0;
    private Speaker currentSpeaker = Speaker.First;
    private bool dialogueCompleted = false;



    public void StartDialogue()
    {
        if (!dialogueCompleted)
        {
            dialogueManager.StartDialogue(dialogue);
            Time.timeScale = 0f;
            dialogueBox.SetActive(true);
            DisplayNextSentence();
            UpdateDialogueSpeaker();
        }
    }

    public void DisplayNextSentence()
    {
        if (currentSpeaker == Speaker.First)
        {
            if (index1 < dialogue.sentences1.Length)
            {
                dialogueManager.characterNameText.text = dialogue.Name1;
                dialogueManager.portraitImage.sprite = dialogue.Image1;
                dialogueManager.dialogueText.text = dialogue.sentences1[index1];
                index1++;
            }
            else
            {
                SwitchSpeaker();
                DisplayNextSentence();
                return;
            }
        }
        else
        {
            if (index2 < dialogue.sentences2.Length)
            {
                dialogueManager.characterNameText.text = dialogue.Name2;
                dialogueManager.portraitImage.sprite = dialogue.Image2;
                dialogueManager.dialogueText.text = dialogue.sentences2[index2];
                index2++;
            }
            else
            {
                EndDialogue();
                return;
            }
        }
    }
    public void SwitchSpeaker()
    {
        if (currentSpeaker == Speaker.First)
        {
            currentSpeaker = Speaker.Second;
        }
        else
        {
            currentSpeaker = Speaker.First;
        }

        UpdateDialogueSpeaker();
    }

    private void UpdateDialogueSpeaker()
    {
        if (currentSpeaker == Speaker.First)
        {
            dialogueManager.characterNameText.text = dialogue.Name1;
            dialogueManager.portraitImage.sprite = dialogue.Image1;
        }
        else
        {
            dialogueManager.characterNameText.text = dialogue.Name2;
            dialogueManager.portraitImage.sprite = dialogue.Image2;
        }
    }

    public void EndDialogue()
    {
        Time.timeScale = 1f;
        dialogueBox.SetActive(false);
        index1 = 0;
        index2 = 0;
        dialogueCompleted = true;
    }

}