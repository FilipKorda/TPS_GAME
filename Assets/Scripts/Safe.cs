using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Safe : MonoBehaviour
{
    private bool canOpen = false;
    public string description = "Press E to PickUp Key";
    public TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject goldObjectToDrop;
    public float delay = 5f;
    public Slider progressBar;
    private float timer;
    private bool isOpened = false;
    private Coroutine countdownCoroutine;
    public bool knowHowToUnlockSafe = false;
    public bool knowCodeToSafeOne = false;
    public bool knowCodeToSafeTwo = false;
    public bool knowCodeToSafeThree = false;
    public bool knowCodeToSafeFourth = false;

    private void Start()
    {
        timer = delay;
    }

    private void Update()
    {
        if (!(isOpened || !canOpen || !knowHowToUnlockSafe || !knowCodeToSafeOne || !Input.GetKeyDown(KeyCode.E))
            || !(isOpened || !canOpen || !knowHowToUnlockSafe || !knowCodeToSafeTwo || !Input.GetKeyDown(KeyCode.E))
            || !(isOpened || !canOpen || !knowHowToUnlockSafe || !knowCodeToSafeThree || !Input.GetKeyDown(KeyCode.E))
            || !(isOpened || !canOpen || !knowHowToUnlockSafe || !knowCodeToSafeFourth || !Input.GetKeyDown(KeyCode.E)))
        {
            if (countdownCoroutine == null)
            {
                descriptionText.text = "";
                countdownCoroutine = StartCoroutine(StartCountdown());
            }
        }
    }

    private IEnumerator StartCountdown()
    {
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
