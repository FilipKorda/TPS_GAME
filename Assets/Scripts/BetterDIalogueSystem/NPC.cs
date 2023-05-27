using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("==== Start Conversation ====")]
    [Space(10)]
    public List<DialogueData> dialogueData = new();
    private int dialogueIndex = 0;
    [Header("==== Final Conversation ====")]
    [Space(10)]
    public List<FinalDialogueData> finalDialogueData = new();
    private int finalDialogueIndex = 0;
    private BetterDialogueManager betterDialogueManager;
    public InteractWithNPC interactWithNPC;
    public bool areChoicesAvailable;
    public bool isDialogueComplete = false;

    [Header("==== Quests ====")]
    [Space(10)]
    public MechanicQuest mechanicGuests;

    private void Start()
    {
        betterDialogueManager = FindObjectOfType<BetterDialogueManager>();
    }

    public void StartDialogue()
    {
        if (!isDialogueComplete)
        {
            if (dialogueData.Count > 0)
            {
                Time.timeScale = 0f;
                interactWithNPC.isInteracting = true;
                betterDialogueManager.dialogueBox.SetActive(true);
                dialogueIndex = 0;
                DisplayDialogue(dialogueData[dialogueIndex]);
            }
        }
        else
        {
            Time.timeScale = 0f;
            interactWithNPC.isInteracting = true;
            betterDialogueManager.dialogueBox.SetActive(true);
            finalDialogueIndex = 0;
            DisplayFinalDialogue(finalDialogueData[finalDialogueIndex]);
        }
    }

    public void NextFinalDialogue()
    {
        finalDialogueIndex++;
        if (finalDialogueIndex < finalDialogueData.Count)
        {
            betterDialogueManager.SetFinalDialogue(finalDialogueData[finalDialogueIndex].name, finalDialogueData[finalDialogueIndex].portrait,
                 finalDialogueData[finalDialogueIndex].sentences, finalDialogueData[finalDialogueIndex]);
            DisplayFinalDialogue(finalDialogueData[finalDialogueIndex]);
        }
        else
        {
            EndDialogue();
        }
    }

    public void NextDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogueData.Count)
        {
            DisplayDialogue(dialogueData[dialogueIndex]);

            if (!dialogueData[dialogueIndex].isQuestion)
            {
                areChoicesAvailable = false;

                if (dialogueIndex == dialogueData.Count - 2 || dialogueIndex == dialogueData.Count - 1)
                {
                    if (!areChoicesAvailable)
                    {
                        EndDialogue();
                    }
                }
            }
            else
            {
                areChoicesAvailable = true;
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

        //MechanicQuest
        MechanicQuestComplete();
    }

    public void OnPlayerChoiceMade(int choiceIndex)
    {
        dialogueIndex = dialogueData[dialogueIndex].afterAnswerIndexes[choiceIndex];
        DisplayDialogue(dialogueData[dialogueIndex]);
    }

    private void DisplayDialogue(DialogueData data)
    {
        betterDialogueManager.SetDialogue(data.name, data.portrait, data.sentences, data, this);
        areChoicesAvailable = data.buttonsAnswers != null && data.buttonsAnswers.Count > 0;
    }

    private void DisplayFinalDialogue(FinalDialogueData data)
    {
        betterDialogueManager.SetFinalDialogue(data.name, data.portrait, data.sentences, data);
    }

    private void MechanicQuestComplete()
    {
        if (isDialogueComplete)
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
