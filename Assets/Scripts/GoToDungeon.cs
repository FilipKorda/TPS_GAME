using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToDungeon : MonoBehaviour
{
    private bool canTeleport = false;
    public string description = "Press E to enter House";
    public TextMeshProUGUI descriptionText;

    [Header("==== Fades ====")]
    [Space(10)]
    private float fadeDuration = 1.2f;
    public Image fadeOutEnterDungeon;
    private float fadeDelay = 1f;

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
            descriptionText.text = "";
        }
    }
    IEnumerator Teleport()
    {
        Time.timeScale = 0;
        float t = 0;
        Color originalColor = fadeOutEnterDungeon.color;

        while (t < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1f, t / fadeDuration);
            fadeOutEnterDungeon.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        fadeOutEnterDungeon.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        yield return new WaitForSecondsRealtime(fadeDelay);
        SceneManager.LoadScene("Level01");
        Time.timeScale = 1;
    }

}
