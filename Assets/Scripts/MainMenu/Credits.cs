using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public TextMeshProUGUI creditsText;
    public float scrollSpeed = 50f;
    public float creditsHeight = 2550f; //tu w zale¿noœci jak d³ugie s¹ creditsy
    public float SpaceBewtweenTitleAName = 40f;
    public float spaceBetweenNames = 25f;
    public float groupFontSize = 30f;
    public bool isCreditsScrollFinished = false;
    private Vector2 originalPos;
    public Image fadeOutPanel;
    private Color originalPanelColor;
    public bool inCreditsPanel = false;
    [SerializeField] private MainMenuController mainMenuController;
    [Header("==== GROUPS AND NAMES ====")]
    [Space(10)]
    public List<string> groups = new List<string>() { "Group 1", "Group 2", "Group 3" };
    public List<Color> groupColors = new List<Color>() { Color.white, Color.white, Color.white };

    [Serializable]
    public class NamesList
    {
        public List<string> names = new List<string>();
    }    

    [SerializeField]
    private List<NamesList> names = new List<NamesList>()
    {
         new NamesList() { names = new List<string>() { "Name 1", "Name 2", "Name 3" } },
         new NamesList() { names = new List<string>() { "Name 4", "Name 5", "Name 6", "Name 7" } },
         new NamesList() { names = new List<string>() { "Name 8", "Name 9" } },
         new NamesList() { names = new List<string>() { "Name 10", "Name 11" } }
    };

    void Start()
    {
        originalPos = creditsText.rectTransform.anchoredPosition;
        originalPanelColor = fadeOutPanel.color;

        string credits = "";
        for (int i = 0; i < groups.Count; i++)
        {           
            string colorTag = "<color=#" + ColorUtility.ToHtmlStringRGBA(groupColors[i]) + ">";
            credits += "<size=" + groupFontSize + ">" + colorTag + groups[i] + "</color>" + "</size>\n" + "<line-height=" + SpaceBewtweenTitleAName + ">\n";
            for (int j = 0; j < names[i].names.Count; j++)
            {
                credits += "  " + names[i].names[j] + "\n";
            }
            if (i < groups.Count - 1)
            {
                credits += "<line-height=" + spaceBetweenNames + ">\n";
            }        

        }
        creditsText.text = credits;      

    }
    IEnumerator FadeOutPanel()
    {
        float duration = 1f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            fadeOutPanel.color = new Color(originalPanelColor.r, originalPanelColor.g, originalPanelColor.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        fadeOutPanel.color = originalPanelColor;
        inCreditsPanel = false;
        mainMenuController.inMainMenu = true;
        isCreditsScrollFinished = false;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        creditsText.transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        if (creditsText.rectTransform.anchoredPosition.y >= creditsHeight)
        {

            creditsText.rectTransform.anchoredPosition = new Vector2(creditsText.rectTransform.anchoredPosition.x, -creditsHeight);
            isCreditsScrollFinished = true;
        }
        if (isCreditsScrollFinished)
        {
            creditsText.rectTransform.anchoredPosition = originalPos;
            StartCoroutine(FadeOutPanel());
        }        
    }
}
