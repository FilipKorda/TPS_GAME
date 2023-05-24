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
    public GameObject ButtonsPanel;
    public Button button1;
    public Button button2;
    private Queue<string> sentenceQueue;
    private bool isDisplayingSentence = false;

    private DialogueQuestion dialogue;
    private AfterYesAswer afterYesAswer;
    private AfterNoAswer afterNoAswer;


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
    public void StartAfterYesAnswerDialogue()
    {
        characterNameText.text = afterYesAswer.Name;
        portraitImage.sprite = afterYesAswer.Portrait;
        dialogueText.text = afterYesAswer.Sentences;
    }
    public void StartAfterNoAnswerDialogue()
    {
        characterNameText.text = afterNoAswer.Name;
        portraitImage.sprite = afterNoAswer.Portrait;
        dialogueText.text = afterNoAswer.Sentences;
    }
    public void DisplayQuestion(DialogueQuestion question)
    {
        characterNameText.text = question.Name;
        portraitImage.sprite = question.Portrait;
        sentenceQueue.Clear();
        dialogueText.text = question.question;

        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        if (question.answers.Count >= 1)
        {
            button1.gameObject.SetActive(true);
            button1.GetComponentInChildren<TextMeshProUGUI>().text = question.answers[0].answer;
            button1.onClick.RemoveAllListeners();
            button1.onClick.AddListener(() => OnAnswerSelected(question.answers[0]));
        }

        if (question.answers.Count >= 2)
        {
            button2.gameObject.SetActive(true);
            button2.GetComponentInChildren<TextMeshProUGUI>().text = question.answers[1].answer;
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(() => OnAnswerSelected(question.answers[1]));
        }

        ButtonsPanel.SetActive(true);
        isDisplayingSentence = false;
        DisplayNextSentence();
    }
    public void OnAnswerSelected(DialogueAnswer answer)
    {
        Debug.Log("Wybrano odpowiedü: " + answer.answer);
        if (answer.answer == "Tak")
        {
            HideButtonsPanel();
            if (dialogue.afterYesAnswer != null)
            {
                StartAfterYesAnswerDialogue();
                HideButtonsPanel();
            }
            else
            {
                // Brak dalszego dialogu po odpowiedzi "Yes"
                NextDialogue();
            }
            
        }
        else if (answer.answer == "Nie")
        {
            
            if (dialogue.afterNoAnswer != null)
            {
                StartAfterNoAnswerDialogue();
                HideButtonsPanel();
            }
            else
            {
                // Brak dalszego dialogu po odpowiedzi "No"
                NextDialogue();
            }
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
            NextDialogue();
            return;
        }

        string sentence = sentenceQueue.Dequeue();
        dialogueText.text = sentence;

        if (sentenceQueue.Count == 0)
        {
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        Debug.Log("Nastepny dialog");
    }

}
