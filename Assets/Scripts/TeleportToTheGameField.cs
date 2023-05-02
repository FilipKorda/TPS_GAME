using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TeleportToTheGameField : MonoBehaviour
{
    [Header("==== Another Scripts ====")]
    [Space(10)]
    public CheckDestroyedObjects checkDestroyedObjects;
    [Header("==== Fades ====")]
    [Space(10)]
    private float fadeDuration = 0.3f;
    private float fadeDelay = 1f;
    public Image FadeOutInPanelImage;
    [Header("==== Others ====")]
    [Space(10)]
    public Transform teleportTarget;
    public GameObject player;
    public GameObject teleport;
    private bool canTeleport = false;
    public string description = "Press E to teleport";
    public TextMeshProUGUI descriptionText;
     
    


    void Start()
    {
        FadeOutInPanelImage.color = new Color(0, 0, 0, 0);
        teleport.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;
            descriptionText.enabled = true;
            descriptionText.text = description;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = false;
            descriptionText.enabled = false;
        }
    }
    void Update()
    {
        if (canTeleport && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Teleport());
        }
    }
    IEnumerator Teleport()
    {
        Time.timeScale = 0;

        float t = 0;
        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1f, t / fadeDuration);
            FadeOutInPanelImage.color = new Color(0, 0, 0, alpha);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        FadeOutInPanelImage.color = new Color(0, 0, 0, 1f);

        player.transform.position = teleportTarget.position;
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(fadeDelay);

        t = 0;
        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0, t / fadeDuration);
            FadeOutInPanelImage.color = new Color(0, 0, 0, alpha);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        FadeOutInPanelImage.color = new Color(0, 0, 0, 0);
        teleport.SetActive(false);

        //tu zamieniasz boola z skryptu CheckDestroyedObjects na false ¿eybœ móg³ ponownie wróciæ do sklepu i kupiæ coœ
        checkDestroyedObjects.anyObjectDestroyed = false;
    }
}
