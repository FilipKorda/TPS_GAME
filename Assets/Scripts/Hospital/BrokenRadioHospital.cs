using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BrokenRadioHospital : MonoBehaviour
{
    private bool canRepair = false;
    public string description = "Press E to Radio";
    public TextMeshProUGUI descriptionText;
    public string isDestroyDescription = "Radio jest zniszczone";
    public TextMeshProUGUI isDestroyDescriptionText;
    public GameObject brokenRadio;
    public GameObject newRadio;
    public float delay = 5f;
    public Slider progressBar;
    private float timer;
    private bool isRepaired = false;
    private Coroutine countdownCoroutine;
    public TextMeshProUGUI repairingText;

    private void Start()
    {
        timer = delay;

    }
    private void Update()
    {
        if (canRepair && Input.GetKeyDown(KeyCode.E))
        {
            if (ObjectToCompleteRadioQuest.haveGears && ObjectToCompleteRadioQuest.haveScrews && ObjectToCompleteRadioQuest.haveSpeaker && ObjectToCompleteRadioQuest.haveWires)
            {
                if (countdownCoroutine == null)
                {
                    descriptionText.text = "";
                    countdownCoroutine = StartCoroutine(StartCountdown());
                }
            }
            else if (!ObjectToCompleteRadioQuest.haveGears && !ObjectToCompleteRadioQuest.haveScrews && !ObjectToCompleteRadioQuest.haveSpeaker && !ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            //masz jedna rzecz
            else if (!ObjectToCompleteRadioQuest.haveGears && !ObjectToCompleteRadioQuest.haveScrews && !ObjectToCompleteRadioQuest.haveSpeaker && ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            else if (!ObjectToCompleteRadioQuest.haveGears && !ObjectToCompleteRadioQuest.haveScrews && ObjectToCompleteRadioQuest.haveSpeaker && !ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            else if (!ObjectToCompleteRadioQuest.haveGears && ObjectToCompleteRadioQuest.haveScrews && !ObjectToCompleteRadioQuest.haveSpeaker && !ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            else if (ObjectToCompleteRadioQuest.haveGears && !ObjectToCompleteRadioQuest.haveScrews && !ObjectToCompleteRadioQuest.haveSpeaker && !ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            //masz 2 rzeczy
            else if (!ObjectToCompleteRadioQuest.haveGears && !ObjectToCompleteRadioQuest.haveScrews && ObjectToCompleteRadioQuest.haveSpeaker && ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            else if (!ObjectToCompleteRadioQuest.haveGears && ObjectToCompleteRadioQuest.haveScrews && ObjectToCompleteRadioQuest.haveSpeaker && !ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            else if (ObjectToCompleteRadioQuest.haveGears && ObjectToCompleteRadioQuest.haveScrews && !ObjectToCompleteRadioQuest.haveSpeaker && !ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            else if (ObjectToCompleteRadioQuest.haveGears && !ObjectToCompleteRadioQuest.haveScrews && !ObjectToCompleteRadioQuest.haveSpeaker && ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            //masz 3 rzeczy
            else if (!ObjectToCompleteRadioQuest.haveGears && ObjectToCompleteRadioQuest.haveScrews && ObjectToCompleteRadioQuest.haveSpeaker && ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            else if (ObjectToCompleteRadioQuest.haveGears && ObjectToCompleteRadioQuest.haveScrews && ObjectToCompleteRadioQuest.haveSpeaker && !ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            else if (ObjectToCompleteRadioQuest.haveGears && ObjectToCompleteRadioQuest.haveScrews && !ObjectToCompleteRadioQuest.haveSpeaker && ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
            else if (ObjectToCompleteRadioQuest.haveGears && !ObjectToCompleteRadioQuest.haveScrews && ObjectToCompleteRadioQuest.haveSpeaker && ObjectToCompleteRadioQuest.haveWires)
            {
                ShowDestroyDescription();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canRepair = true;
            descriptionText.text = description;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canRepair = false;
            descriptionText.text = "";
        }
    }
    private IEnumerator StartCountdown()
    {
        Time.timeScale = 0;
        isRepaired = false;
        progressBar.gameObject.SetActive(true);
        repairingText.gameObject.SetActive(true);
        while (timer > 0f)
        {
            timer -= Time.unscaledDeltaTime;
            progressBar.value = 1f - (timer / delay);
            yield return null;
        }
        progressBar.gameObject.SetActive(false);
        repairingText.gameObject.SetActive(false);
        brokenRadio.SetActive(false);
        newRadio.SetActive(true);
        isRepaired = true;
        Time.timeScale = 1;
        countdownCoroutine = null;
    }
    IEnumerator HideWhatYouGotText(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDestroyDescriptionText.text = "";
    }

    private void ShowDestroyDescription()
    {
        isDestroyDescriptionText.text = isDestroyDescription;
        StartCoroutine(HideWhatYouGotText(1.5f));
    }
}
