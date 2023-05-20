using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPlayer : MonoBehaviour
{
    [Header("==== Others ====")]
    [Space(10)]
    public Transform center;
    public float teleportDistance = 10f;
    public string description = "Press E to teleport to Center";
    public TextMeshProUGUI descriptionText;
    private bool isTeleporting = false;
    [Header("==== Fades ====")]
    [Space(10)]
    private float fadeDuration = 0.3f;
    private float fadeDelay = 1f;
    public Image fadeOutInPanelImage;

    private void Update()
    {
        if(center != null)
        {
            float distance = Vector3.Distance(transform.position, center.position);
            if (distance > teleportDistance)
            {
                descriptionText.text = description;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!isTeleporting)
                    {
                        StartCoroutine(Teleport());
                        descriptionText.text = "";
                    }
                }
            }
            else
            {
                descriptionText.text = "";
            }
        }
        
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
        transform.position = center.position;
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
