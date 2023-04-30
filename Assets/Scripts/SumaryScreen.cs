using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SumaryScreen : MonoBehaviour
{
    [Header("==== Lista Buttonów ====")]
    [Space(10)]
    [SerializeField] private Button[] buttons;
    [Header("==== Buttony ====")]
    [Space(10)]
    [SerializeField] public Button continueButton;
    [Header("==== UI Elements ====")]
    [Space(10)]
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI totalScore;  
    public GameObject sumaryPanel;  
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ExperienceSystem experienceSystem;
    [Header("==== Anothers ====")]
    [Space(10)]
   // [SerializeField] private string mainMenuScene;
    public bool isSumaryScreenOpen = false;
    private int currentIndex = 0;
    private bool isMouseOverButton = false;


    void Update()
    {
        UpdateStatsDisplay();
        HandleSumaryPanelInput();
        SetButtonToFirst();
    }
    private int GetTimeInSeconds()
    {    
        int minutes = int.Parse(gameManager.timerText.text[..2]);
        int seconds = int.Parse(gameManager.timerText.text.Substring(3, 2));
        return minutes * 60 + seconds;
    }
    public void UpdateStatsDisplay()
    {
        killsText.text = "" + gameManager.numKills.ToString();
        damage.text = "" + experienceSystem.NewDamage.ToString();
        currentLevel.text = "" + experienceSystem.currentLevel.ToString();
        timerText.text = gameManager.timerText.text;
        int score = (gameManager.numKills + experienceSystem.currentLevel) * 2 + (experienceSystem.NewDamage + experienceSystem.currentXP) * 2 + experienceSystem.xpToNextLevel / 2 + GetTimeInSeconds();
        if (score < 0)
        {
            score = 0;
        }
        totalScore.text = score.ToString();
    }
    public void SetButtonToFirst()
    {
        if (isSumaryScreenOpen)
        {
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
            ToggleAttachedObject(buttons[currentIndex], true);
        }
    }
    public void HandleSumaryPanelInput()
    {
        if (isSumaryScreenOpen)
        {
            Button selectedButton = buttons[currentIndex];
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                AudioManager.Instance.PlaySFX("ButtonSound");
                ToggleAttachedObject(selectedButton, false);
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = buttons.Length - 1;
                }

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                AudioManager.Instance.PlaySFX("ButtonSound");
                ToggleAttachedObject(selectedButton, false);
                currentIndex++;
                if (currentIndex >= buttons.Length)
                {
                    currentIndex = 0;
                }
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                bool anyButtonHovered = false;

                for (int i = 0; i < buttons.Length; i++)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(buttons[i].GetComponent<RectTransform>(), Input.mousePosition))
                    {
                        if (!isMouseOverButton)
                        {
                            AudioManager.Instance.PlaySFX("ButtonSound");
                            isMouseOverButton = true;
                        }
                        anyButtonHovered = true;

                        ToggleAttachedObject(selectedButton, false);
                        currentIndex = i;
                        selectedButton = buttons[currentIndex];
                        ToggleAttachedObject(selectedButton, true);
                        break;
                    }

                }
                if (!anyButtonHovered)
                {
                    isMouseOverButton = false;
                }

            }
            else
            {
                isMouseOverButton = false;
            }

            Submit();
        }
    }
    public void OnContinueButtonClick()
    {
        if (isSumaryScreenOpen)
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            Debug.Log("Continue");
            SceneManager.LoadScene("MainMenu");
            if (sumaryPanel.activeSelf == true)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], true);
            }
        }
    }
    private void ToggleAttachedObject(Button button, bool state)
    {
        GameObject attachedObject = button.gameObject.transform.GetChild(0).gameObject;
        attachedObject.SetActive(state);
    }
    private void Submit()
    {
        Button selectedButton = buttons[currentIndex];
        if (Input.GetKeyDown(KeyCode.X))
        {

            if (selectedButton == continueButton)
            {
                continueButton.onClick.Invoke();
            }
            
        }
    }
}
