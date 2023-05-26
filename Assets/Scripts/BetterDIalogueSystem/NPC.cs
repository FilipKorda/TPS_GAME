using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("==== Conversation ====")]
    [Space(10)]
    public List<DialogueData> dialogueData = new();
    private int dialogueIndex = 0;
    [Header("==== Conversation ====")]
    [Space(10)]
    public List<FinalDialogueData> finalDialogueData = new();
    private int finalDialogueIndex = 0;   
    private BetterDialogueManager betterDialogueManager;
    public InteractWithNPC interactWithNPC;
    public bool areChoicesAvailable;
    public bool isDialogueComplete = false;

    private void Start()
    {
        betterDialogueManager = FindObjectOfType<BetterDialogueManager>();
    }

    public void StartDialogue()
    {
        if (dialogueData.Count > 0)
        {
            Time.timeScale = 0f;
            interactWithNPC.isInteracting = true;
            betterDialogueManager.dialogueBox.SetActive(true);
            dialogueIndex = 0;
            DisplayDialogue(dialogueData[dialogueIndex]);

            if (dialogueData[dialogueIndex].buttonsAnswers != null && dialogueData[dialogueIndex].buttonsAnswers.Count > 0)
            {
                betterDialogueManager.SetDialogue(dialogueData[dialogueIndex].name, dialogueData[dialogueIndex].portrait,
                    dialogueData[dialogueIndex].sentences, dialogueData[dialogueIndex], this);
            }
        }
        if(isDialogueComplete)
        {
            DisplayFinalDialogue();
        }
        
    }

    public void NextDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogueData.Count)
        {
            // Sprawdzenie, czy s¹ dostêpne wybory
            if (dialogueData[dialogueIndex].buttonsAnswers != null && dialogueData[dialogueIndex].buttonsAnswers.Count > 0)
            {
                // Przekazanie informacji o danych dialogowych i skrypcie NPC do BetterDialogueManager
                betterDialogueManager.SetDialogue(dialogueData[dialogueIndex].name, dialogueData[dialogueIndex].portrait,
                    dialogueData[dialogueIndex].sentences, dialogueData[dialogueIndex], this);
                areChoicesAvailable = true;
            }
            else
            {
                DisplayDialogue(dialogueData[dialogueIndex]);
                areChoicesAvailable = false;

                // Sprawdzenie, czy to jest przedostatni lub ostatni dialog i koñczy dialog
                if (dialogueIndex == dialogueData.Count - 2 || dialogueIndex == dialogueData.Count - 1)
                {
                    EndDialogue();
                }
            }
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        betterDialogueManager.dialogueBox.SetActive(false);
        Time.timeScale = 1f;
        interactWithNPC.isInteracting = false;
        isDialogueComplete = true;


    }
    public void DisplayFinalDialogue()
    {
        if (finalDialogueIndex < finalDialogueData.Count)
        {
            FinalDialogueData selectedFinalDialogue = finalDialogueData[finalDialogueIndex];
            betterDialogueManager.SetDialogue(selectedFinalDialogue.name, selectedFinalDialogue.portrait,
                selectedFinalDialogue.sentences, selectedFinalDialogue);

            finalDialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void OnPlayerChoiceMade(int choiceIndex)
    {
        dialogueIndex = dialogueData[dialogueIndex].afterAnswerIndexes[choiceIndex];
        DisplayDialogue(dialogueData[dialogueIndex]);
    }   

    private void DisplayDialogue(DialogueData data)
    {
        betterDialogueManager.SetDialogue(data.name, data.portrait, data.sentences, data, this);
    }

}
