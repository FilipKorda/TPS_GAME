using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LockerSearch : MonoBehaviour
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


    private void Start()
    {
        timer = delay;
    }

    private void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.E))
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
        float bounceVelocity = 2f;
        Vector2 bounceDirection = new Vector2(0.1f, -1f).normalized;
        rb.velocity = bounceDirection * bounceVelocity;
        rb.isKinematic = true;
        float timeUntilRemove = 0.5f;
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
