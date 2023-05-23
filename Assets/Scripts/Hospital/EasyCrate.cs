using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EasyCrate : MonoBehaviour
{
    private bool canOpen = false;
    public string description = "Press E to Open Crate";
    public string dontHaveDescription = "Nie masz m³ota";
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI dontHaveDescriptionText;
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
        if (canOpen && Input.GetKeyDown(KeyCode.E) && PickUpHospitalQuest.haveHammer)
        {
            if (countdownCoroutine == null)
            {
                descriptionText.text = "";
                AudioManager.Instance.PlaySFX("EasyCrateDestroing");
                countdownCoroutine = StartCoroutine(StartCountdown());
            }
        }
        else if (canOpen && !PickUpHospitalQuest.haveHammer && Input.GetKeyDown(KeyCode.E))
        {
            dontHaveDescriptionText.text = dontHaveDescription;
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
        DestroyCrate();
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
    private void DestroyCrate()
    {
        Destroy(gameObject);
        AudioManager.Instance.PlaySFX("EasyCrateDestroy");
        DropObjectForPlayer();
    }

    IEnumerator HideWhatYouGotText(float delay)
    {
        yield return new WaitForSeconds(delay);
        dontHaveDescriptionText.text = "";
    }
}
