using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterTheBankScript : MonoBehaviour
{
    [Header("==== Others ====")]
    [Space(10)]
    public Transform teleportTarget;
    public GameObject player;
    private bool canTeleport = false;
    public bool haveKey = false;
    public string description = "Press E to enter House";
    public string dontHaveKeyDescription = "Press E to enter House";
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI dontHaveKeyDescriptionText;
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
        if (haveKey && canTeleport && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Teleport());
        }
        if(!haveKey && canTeleport && Input.GetKeyDown(KeyCode.E))
        {
            dontHaveKeyDescriptionText.text = dontHaveKeyDescription;
            StartCoroutine(HideDontHaveKeyDescription(3f));
        }
    }

    IEnumerator HideDontHaveKeyDescription(float delay)
    {
        yield return new WaitForSeconds(delay);
        dontHaveKeyDescriptionText.text = "";
    }
    IEnumerator Teleport()
    {

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
