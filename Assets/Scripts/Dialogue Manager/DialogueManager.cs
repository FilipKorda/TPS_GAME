using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;



public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;
    public GameObject ButtonsPanel;
    public Button button1;
    public Button button2;

    private Queue<string> sentenceQueue;
    private bool isDisplayingSentence = false;

    private bool isWaitingForChoice = false;
    private DialogueChoice currentChoice;

    public void StartDialogue(Dialogue dialogue)
    {
        characterNameText.text = dialogue.name;
        portraitImage.sprite = dialogue.portrait;
        sentenceQueue = new Queue<string>();
        isWaitingForChoice = false;
        currentChoice = null;

        if (dialogue.isQuestion)
        {

            Debug.Log("Showing choice buttons");
            isWaitingForChoice = true;
            ShowChoiceButtons(dialogue.choices);
        }
        else
        {
            EnqueueSentences(dialogue.sentences);
            DisplayNextSentence();
        }
    }


    public void StartLastDialogue(LastDialogue lastDialogue)
    {
        characterNameText.text = lastDialogue.lastName;
        portraitImage.sprite = lastDialogue.lastPortrait;
        sentenceQueue = new Queue<string>();
        isWaitingForChoice = false;
        currentChoice = null;

        if (lastDialogue.choices != null && lastDialogue.choices.Count > 0)
        {
            isWaitingForChoice = true;
            ShowChoiceButtons(lastDialogue.choices);
        }
        else
        {
            EnqueueSentences(lastDialogue.lastSentences);
            DisplayNextSentence();
        }
    }

    public void HideButtonsPanel()
    {
        ButtonsPanel.SetActive(false);
    }

    public void DisplayNextSentence()
    {
        if (isDisplayingSentence)
        {
            return;
        }

        if (sentenceQueue.Count == 0)
        {
            if (isWaitingForChoice)
            {
                HideChoiceButtons();
                ProcessChoice(currentChoice);
            }

            return;
        }

        string sentence = sentenceQueue.Dequeue();
        dialogueText.text = sentence;

        if (sentenceQueue.Count == 0)
        {
            if (isWaitingForChoice)
            {
                HideChoiceButtons();
                ProcessChoice(currentChoice);
            }
        }
    }


    private void ShowChoiceButtons(List<DialogueChoice> choices)
    {
        ButtonsPanel.SetActive(true);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        button1.onClick.AddListener(() => ChooseOption(choices[0]));
        button2.onClick.AddListener(() => ChooseOption(choices[1]));

        button1.GetComponentInChildren<TextMeshProUGUI>().text = choices[0].optionText;
        button2.GetComponentInChildren<TextMeshProUGUI>().text = choices[1].optionText;
    }

    private void HideChoiceButtons()
    {
        ButtonsPanel.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
    }

    private void ChooseOption(DialogueChoice choice)
    {
        currentChoice = choice;
        isWaitingForChoice = false;
        EnqueueSentences(choice.sentences);
        DisplayNextSentence();
    }

    private void EnqueueSentences(List<string> sentences)
    {
        foreach (string sentence in sentences)
        {
            sentenceQueue.Enqueue(sentence);
        }
    }

    private void ProcessChoice(DialogueChoice choice)
    {
        // Wykonaj odpowiednie czynnoœci na podstawie wyboru dokonanego przez gracza
        // Mo¿esz dodaæ dodatkow¹ logikê tutaj w zale¿noœci od wyboru
    }
}
