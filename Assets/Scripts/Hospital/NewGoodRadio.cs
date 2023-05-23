using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewGoodRadio : MonoBehaviour
{
    private bool canInteract = false;
    public string descriptionOn = "Press E to turn On Radio";
    public string descriptionOff = "Press E to turn Off Radio <br> Press X to turn Listen Radio";
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI radioDescriptionText;
    public GameObject checkMark;
    public GameObject ListenRadio;
    public RadioDialogue radioDialogue;
    private Coroutine displayCoroutine;
    public PlayerController playerController;
    public static bool radioOn = false;

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            radioOn = !radioOn;

            if (radioOn)
            {
                checkMark.SetActive(true);
            }
            else
            {
                checkMark.SetActive(false);
            }
        }
        if (canInteract && Input.GetKeyDown(KeyCode.X) && radioOn)
        {
            displayCoroutine = StartCoroutine(DisplayRadioSentences());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !radioOn)
        {
            canInteract = true;
            descriptionText.text = descriptionOn;
        }
        else if (other.CompareTag("Player") && radioOn)
        {
            canInteract = true;
            descriptionText.text = descriptionOff;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            descriptionText.text = "";
        }
    }
    private IEnumerator DisplayRadioSentences()
    {
        playerController.enabled = false;
        
        radioDescriptionText.text = "";
        ListenRadio.SetActive(true);
        AudioManager.Instance.PlaySFX("RadioListen");
        foreach (char letter in radioDialogue.radioSentences)
        {
            radioDescriptionText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);
        ListenRadio.SetActive(false);
        StopDisplayingRadioSentences();
        displayCoroutine = null;
    }
    private void StopDisplayingRadioSentences()
    {
        if (displayCoroutine != null)
        {
            playerController.enabled = true;
            StopCoroutine(displayCoroutine);
            displayCoroutine = null;
        }
    }
}
[System.Serializable]
public class RadioDialogue
{
    [TextArea(1, 3)]
    public string radioSentences;
}