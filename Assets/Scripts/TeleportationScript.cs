using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeleportationScript : MonoBehaviour
{
    [Header("==== Others ====")]
    [Space(10)]
    public Transform teleportTarget;
    public GameObject player;
    private bool canTeleport = false;
    public string description = "Press E to enter House";
    public TextMeshProUGUI descriptionText;
    public float speed = 0.215f;
    [Header("==== Fades ====")]
    [Space(10)]
    private float fadeDuration = 0.3f;
    private float fadeDelay = 1f;
    public Image fadeOutInPanelImage;

    void Start()
    {
        fadeOutInPanelImage.color = new Color(0, 0, 0, 0);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;
            descriptionText.text = description;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = false;
            descriptionText.text = "";
        }
    }
    void Update()
    {
        if (canTeleport && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Teleport());
        }
    }

    private void AnimDoorOpen()
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + new Vector3(speed, 0f, 0f);
        transform.position = newPosition;
    }
    private void AnimDoorClosed()
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + new Vector3(-speed, 0f, 0f);
        transform.position = newPosition;
    }
    IEnumerator Teleport()
    {
        AnimDoorOpen();
        Time.timeScale = 0;
        float t = 0;
        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1f, t / fadeDuration);
            fadeOutInPanelImage.color = new Color(0, 0, 0, alpha);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        fadeOutInPanelImage.color = new Color(0, 0, 0, 1f);    
        player.transform.position = teleportTarget.position;
        Time.timeScale = 1;
        AnimDoorClosed();
        yield return new WaitForSecondsRealtime(fadeDelay);       
        t = 0;
        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0, t / fadeDuration);
            fadeOutInPanelImage.color = new Color(0, 0, 0, alpha);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        fadeOutInPanelImage.color = new Color(0, 0, 0, 0);
    }
}
