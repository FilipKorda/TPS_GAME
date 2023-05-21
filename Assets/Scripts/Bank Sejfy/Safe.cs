using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Safe : MonoBehaviour
{
    private bool canOpen = false;
    public string description = "Press E to PickUp Key";
    public string dontKnowDescription = "Nie znasz kodu do: ";
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI dontKnowDescriptionText;
    [SerializeField] private GameObject goldObjectToDrop;
    [SerializeField] private GameObject greenCheckMark;
    public float delay = 5f;
    public Slider progressBar;
    private float timer;
    private bool isOpened = false;
    private Coroutine countdownCoroutine;

    private void Start()
    {
        timer = delay;
    }

    private void Update()
    {
        if (!isOpened && canOpen && CorrectSafeNote.knowCodeToSafeOne && Input.GetKeyDown(KeyCode.E))
        {
            if (countdownCoroutine == null)
            {
                descriptionText.text = "";
                countdownCoroutine = StartCoroutine(StartCountdown());
            }
        }
        else if (!isOpened && canOpen && !CorrectSafeNote.knowCodeToSafeOne && Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.PlaySFX("RejectSafeCode");
            dontKnowDescriptionText.text = dontKnowDescription;
            StartCoroutine(HideWhatYouGotText(2f));
        }
    }

    private IEnumerator StartCountdown()
    {
        AudioManager.Instance.PlaySFX("EnterCodeSafe");
        Time.timeScale = 0;
        progressBar.gameObject.SetActive(true);

        while (timer > 0f)
        {
            timer -= Time.unscaledDeltaTime;
            progressBar.value = 1f - (timer / delay);
            yield return null;
        }

        progressBar.gameObject.SetActive(false);
        SafeIsOpen();
        isOpened = true;
        greenCheckMark.SetActive(true);
        Time.timeScale = 1;
        countdownCoroutine = null;
    }
    IEnumerator HideWhatYouGotText(float delay)
    {
        yield return new WaitForSeconds(delay);
        dontKnowDescriptionText.text = "";
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
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
                countdownCoroutine = null;
            }
            progressBar.gameObject.SetActive(false);
        }
    }
    private void BoucingEffectGoldObject()
    {
        if (goldObjectToDrop == null)
        {
            return;
        }
        GameObject droppedObject = Instantiate(goldObjectToDrop, transform.position, Quaternion.identity);
        Rigidbody2D rb = droppedObject.AddComponent<Rigidbody2D>();
        float bounceVelocity = 1f;
        Vector2 bounceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.velocity = bounceDirection * bounceVelocity;
        rb.isKinematic = true;
        float timeUntilRemove = 0.8f;
        Destroy(rb, timeUntilRemove);
    }
    public void DropGoldObjectForPlayer()
    {
        BoucingEffectGoldObject();
    }
    private void SafeIsOpen()
    {
        Destroy(gameObject);
        DropGoldObjectForPlayer();
    }
}
