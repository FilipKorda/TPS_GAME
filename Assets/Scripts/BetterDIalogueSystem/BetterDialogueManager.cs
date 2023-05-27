using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetterDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image portraitImage;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public GameObject choiceButtonsPanel;
    public Button[] choiceButtons;

    private DialogueData currentDialogueData;
    private FinalDialogueData currentFinalDialogueData;
    private NPC currentNPC;

    public void SetDialogue(string name, Sprite portrait, string sentences, DialogueData dialogueData, NPC npc)
    {
        nameText.text = name;
        portraitImage.sprite = portrait;
        dialogueText.text = sentences;
        currentDialogueData = dialogueData;
        currentNPC = npc;

        if (dialogueData.isQuestion)
        {
            if (dialogueData.buttonsAnswers != null && dialogueData.buttonsAnswers.Count > 0)
            {
                choiceButtonsPanel.SetActive(true);
                for (int i = 0; i < choiceButtons.Length; i++)
                {
                    if (i < dialogueData.buttonsAnswers.Count)
                    {
                        choiceButtons[i].gameObject.SetActive(true);
                        choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = dialogueData.buttonsAnswers[i];
                    }
                    else
                    {
                        choiceButtons[i].gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            choiceButtonsPanel.SetActive(false);
        }
    }


    public void SetFinalDialogue(string name, Sprite portrait, string sentences, FinalDialogueData finalDialogueData)
    {
        nameText.text = name;
        portraitImage.sprite = portrait;
        dialogueText.text = sentences;
        currentFinalDialogueData = finalDialogueData;

        choiceButtonsPanel.SetActive(false);
    }

    public void OnChoiceButtonClicked(int choiceIndex)
    {
        // Wywo³anie odpowiedniej metody w skrypcie NPC w zale¿noœci od wyboru gracza
        if (currentNPC != null)
        {
            currentNPC.areChoicesAvailable = false;
            currentNPC.OnPlayerChoiceMade(choiceIndex);
        }
       
        choiceButtonsPanel.SetActive(false);
    }
}
