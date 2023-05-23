using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HardCrate : MonoBehaviour
{
    private bool canOpen = false;
    public string description = "Press E to Open Crate";
    public string dontHaveDescription = "Nie masz m這ta, d逝ta i υmu";
    public string onlyHaveChisle = "Masz tylko d逝ta";
    public string onlyHaveHammer = "Masz tylko m這ta";
    public string onlyHaveCrowbar = "Masz tylko 這mu";
    public string onlyHaveHammerAndChisle = "Brakuje ci tylko 這mu";
    public string onlyHaveChisleAndCrosbar = "Brakuje ci tylko mlota";
    public string onlyHaveHammerAndCrossbar = "Brakuje ci tylko dluta";
    
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI haveDescriptionText;
    [SerializeField] private GameObject ObjectToDrop;
    public float delay = 5f;
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
        //masz wszystko
        if (canOpen && Input.GetKeyDown(KeyCode.E) && PickUpHospitalQuest.haveHammer && PickUpHospitalQuest.haveChisel && PickUpHospitalQuest.haveCrowbar)
        {
            if (countdownCoroutine == null)
            {
                AudioManager.Instance.PlaySFX("HardCrateDestroying");
                descriptionText.text = "";
                countdownCoroutine = StartCoroutine(StartCountdown());
            }
        }
        // nie masz niczego
        else if (canOpen && !PickUpHospitalQuest.haveHammer && !PickUpHospitalQuest.haveChisel && !PickUpHospitalQuest.haveCrowbar && Input.GetKeyDown(KeyCode.E))
        {
            haveDescriptionText.text = dontHaveDescription;
            StartCoroutine(HideWhatYouGotText(1.5f));
        }
        //masz tylko d逝to
        else if (canOpen && !PickUpHospitalQuest.haveHammer && PickUpHospitalQuest.haveChisel && !PickUpHospitalQuest.haveCrowbar && Input.GetKeyDown(KeyCode.E))
        {
            haveDescriptionText.text = onlyHaveChisle;
            StartCoroutine(HideWhatYouGotText(1.5f));
        }
        //masz tylko mlot
        else if (canOpen && PickUpHospitalQuest.haveHammer && !PickUpHospitalQuest.haveChisel && !PickUpHospitalQuest.haveCrowbar && Input.GetKeyDown(KeyCode.E))
        {
            haveDescriptionText.text = onlyHaveHammer;
            StartCoroutine(HideWhatYouGotText(1.5f));
        }
        //masz tylko 這m
        else if (canOpen && !PickUpHospitalQuest.haveHammer && !PickUpHospitalQuest.haveChisel && PickUpHospitalQuest.haveCrowbar && Input.GetKeyDown(KeyCode.E))
        {
            haveDescriptionText.text = onlyHaveCrowbar;
            StartCoroutine(HideWhatYouGotText(1.5f));
        }
        //masz tylko mlot i dluto
        else if (canOpen && PickUpHospitalQuest.haveHammer && PickUpHospitalQuest.haveChisel && !PickUpHospitalQuest.haveCrowbar && Input.GetKeyDown(KeyCode.E))
        {
            haveDescriptionText.text = onlyHaveHammerAndChisle;
            StartCoroutine(HideWhatYouGotText(1.5f));
        }
        //masz tylko d逝to i 這m
        else if (canOpen && !PickUpHospitalQuest.haveHammer && PickUpHospitalQuest.haveChisel && PickUpHospitalQuest.haveCrowbar && Input.GetKeyDown(KeyCode.E))
        {
            haveDescriptionText.text = onlyHaveChisleAndCrosbar;
            StartCoroutine(HideWhatYouGotText(1.5f));
        }
        //masz tylko m這t 這m
        else if (canOpen && PickUpHospitalQuest.haveHammer && !PickUpHospitalQuest.haveChisel && PickUpHospitalQuest.haveCrowbar && Input.GetKeyDown(KeyCode.E))
        {
            haveDescriptionText.text = onlyHaveHammerAndCrossbar;
            StartCoroutine(HideWhatYouGotText(1.5f));
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
        CrateDestroy();
        isOpened = true;
        Time.timeScale = 1;
        countdownCoroutine = null;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
            descriptionText.text = description;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
            descriptionText.text = "";
        }
    }
    private void BoucingEffectObject()
    {
        if (ObjectToDrop == null)
        {
            return;
        }
        GameObject droppedObject = Instantiate(ObjectToDrop, transform.position, Quaternion.identity);
        Rigidbody2D rb = droppedObject.AddComponent<Rigidbody2D>();
        float bounceVelocity = 2f;
        Vector2 bounceDirection = new Vector2(0f, -1f).normalized;
        rb.velocity = bounceDirection * bounceVelocity;
        rb.isKinematic = true;
        float timeUntilRemove = 0.5f;
        Destroy(rb, timeUntilRemove);
    }
    public void DropObjectForPlayer()
    {
        BoucingEffectObject();
    }
    private void CrateDestroy()
    {
        AudioManager.Instance.PlaySFX("HardCrateDestroy");
        Destroy(gameObject);
        DropObjectForPlayer();
    }

    IEnumerator HideWhatYouGotText(float delay)
    {
        yield return new WaitForSeconds(delay);
        haveDescriptionText.text = "";
    }
}
