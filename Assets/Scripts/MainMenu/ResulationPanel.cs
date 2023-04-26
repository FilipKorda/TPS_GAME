using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResulationPanel : MonoBehaviour
{
    [Header("==== Lista Buttonów ====")]
    [Space(10)]
    [SerializeField] public Button[] buttons;
    [Header("==== Buttony ====")]
    [Space(10)]
    [SerializeField] public Button yESButton;
    [SerializeField] public Button nOButton;
    [Header("==== UI Elements ====")]
    [Space(10)]
    [SerializeField] private GameObject acceptResolutionPanel;
    [SerializeField] private GameObject graphicPanelGameObject;
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private GraphicPanel graphicPanel;   
    [Header("==== Anothers ====")]
    [Space(10)]
    [SerializeField] private float inactivityTimer = 5.0f;
    private bool timerActive = false;
    public bool resulationPanelIsActive = false;
    private int currentIndex = 0;
    private bool isMouseOverButton = false;

    private void Update()
    {
        HandleResulationPanelInput();
        SetButtonToFirstS();

        if (resulationPanelIsActive)
        {
            if (!timerActive)
            {
                timerActive = true;
                StartCoroutine(StartTimer());
            }
        }
       
    }
    private IEnumerator StartTimer()
    {
        while (inactivityTimer > 0)
        {
            inactivityTimer = Mathf.Max(0, inactivityTimer - Time.deltaTime);
            yield return null;
        }

        CheckForInactivity();
        timerActive = false;
    }
    private void ResetTimer()
    {
        inactivityTimer = 5f;
    }
    private void CheckForInactivity()
    {       
        acceptResolutionPanel.SetActive(false);
        resulationPanelIsActive = false;
        graphicPanelGameObject.SetActive(true);
        graphicPanel.inGraphicPanel = true;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
        if (inactivityTimer == 0)
        {
            ResetTimer();
        }
    }
    public void SetButtonToFirstS()
    {
        if (resulationPanelIsActive)
        {
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
            ToggleAttachedObject(buttons[currentIndex], true);
        }
    }
    private void HandleResulationPanelInput()
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
            if (!anyButtonHovered) // if no button is hovered, set the boolean to false
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
    private void Submit()
    {
        Button selectedButton = buttons[currentIndex];
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (selectedButton == yESButton)
            {
                yESButton.onClick.Invoke();
            }
            else if (selectedButton == nOButton)
            {
                nOButton.onClick.Invoke();
            }

        }
    }
    public void YesButtonClick()
    {

        AudioManager.Instance.PlaySFX("ApplyButton");
        resulationPanelIsActive = false;
        acceptResolutionPanel.SetActive(false);
        graphicPanel.inGraphicPanel = true;
        Debug.Log("AcceptedSetings");
        timerActive = false;
        ResetTimer();
        if (acceptResolutionPanel.activeSelf == false)
        {
            currentIndex = 1;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void NoButtonClick()
    {
        AudioManager.Instance.PlaySFX("ApplyButton");
        resulationPanelIsActive = false;
        acceptResolutionPanel.SetActive(false);
        graphicPanel.inGraphicPanel = true;
        QualitySettings.SetQualityLevel(0);
        Debug.Log("DiscardSetings");
        timerActive = false;
        ResetTimer();
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, 0);
        if (acceptResolutionPanel.activeSelf == false)
        {
            currentIndex = 1;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void ToggleAttachedObject(Button button, bool state)
    {
        GameObject attachedObject = button.gameObject.transform.GetChild(0).gameObject;
        attachedObject.SetActive(state);
    }


}
