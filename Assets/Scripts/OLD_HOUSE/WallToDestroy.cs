using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WallToDestroy : MonoBehaviour
{
    private bool canDestroy = false;
    public string description = "Press E to Destroy Wall";
    public TextMeshProUGUI descriptionText;
    public string dontHaveDescription = "Nie masz kilofa";
    public TextMeshProUGUI haveDescriptionText;
    public float delay = 2.5f;
    public Slider progressBar;
    private float timer;
    private bool isOpened = false;
    private Coroutine countdownCoroutine;
    public TextMeshProUGUI destroyingText;


    private void Start()
    {
        timer = delay;
    }
    private void Update()
    {
        if (canDestroy && Input.GetKeyDown(KeyCode.E) && Pickaxe.havePickaxe)
        {
            if (countdownCoroutine == null)
            {
                //AudioManager.Instance.PlaySFX("HardCrateDestroying");
                descriptionText.text = "";
                countdownCoroutine = StartCoroutine(StartCountdown());
            }           
        }
        else if (canDestroy && Input.GetKeyDown(KeyCode.E) && !Pickaxe.havePickaxe)
        {
            haveDescriptionText.text = dontHaveDescription;
            StartCoroutine(HideWhatYouGotText(1.5f));
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canDestroy = true;
            descriptionText.text = description;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canDestroy = false;
            descriptionText.text = "";
        }
    }
    private IEnumerator StartCountdown()
    {
        Time.timeScale = 0;
        isOpened = false;
        progressBar.gameObject.SetActive(true);
        destroyingText.gameObject.SetActive(true);
        while (timer > 0f)
        {
            timer -= Time.unscaledDeltaTime;
            progressBar.value = 1f - (timer / delay);
            yield return null;
        }
        progressBar.gameObject.SetActive(false);
        destroyingText.gameObject.SetActive(false);
        isOpened = true;     
        Time.timeScale = 1;
        Destroy(transform.parent.gameObject);
        countdownCoroutine = null;
    }
    IEnumerator HideWhatYouGotText(float delay)
    {
        yield return new WaitForSeconds(delay);
        haveDescriptionText.text = "";
    }
}

