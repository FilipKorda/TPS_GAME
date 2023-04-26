using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlsPanel : MonoBehaviour
{
    [Header("==== Lista Buttonów ====")]
    [Space(10)]
    [SerializeField] public Button[] buttons;
    [Header("==== Buttony ====")]
    [Space(10)]
    [SerializeField] public Button backButton;
    [Header("==== UI Elements ====")]
    [Space(10)]
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject settingsImage;
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private SettingsPanelController settingsPanelController;
    [SerializeField] private PauseMenu pauseMenu;
    [Header("==== Anothers ====")]
    [Space(10)]
    private int currentIndex = 0;
    public bool inControlPanel = false;
    private bool isMouseOverButton = false;

    private void Update()
    {
        HandleControlPanelInput();
        SetButtonToFirstS();
    }
    private void HandleControlPanelInput()
    {

        Button selectedButton = buttons[currentIndex];
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ToggleAttachedObject(selectedButton, false);
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = buttons.Length - 1;
            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
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
    public void SetButtonToFirstS()
    {
        if (inControlPanel)
        {
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
            ToggleAttachedObject(buttons[currentIndex], true);
        }
    }
    private void Submit()
    {
        Button selectedButton = buttons[currentIndex];
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (selectedButton == backButton)
            {
                backButton.onClick.Invoke();
            }
        }
    }
    public void OnBackButtonClick()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            settingsImage.SetActive(true);
            settingsPanelController.grayOutPanel.SetActive(false);
            controlPanel.SetActive(false);
            inControlPanel = false;
            settingsPanelController.inSettingsPanel = true;
            if (!inControlPanel)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            if (controlPanel.activeSelf == false)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], false);
            }
        }

        if (SceneManager.GetActiveScene().name == "LEVEL")
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            pauseMenu.grayOutPanel.SetActive(false);
            controlPanel.SetActive(false);
            inControlPanel = false;
            pauseMenu.inPauseMenu = true;
            if (!inControlPanel)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            if (controlPanel.activeSelf == false)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], false);
            }
        }
    }
    public void ToggleAttachedObject(Button button, bool state)
    {
        GameObject attachedObject = button.gameObject.transform.GetChild(0).gameObject;
        attachedObject.SetActive(state);
    }
}
