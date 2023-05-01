using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("==== Lista Buttonów ====")]
    [Space(10)]
    [SerializeField] private Button[] buttons;
    [Header("==== Buttony ====")]
    [Space(10)]
    [SerializeField] public Button playButton;
    [SerializeField] public Button settingsButton;
    [SerializeField] private Button extrasButton;
    [SerializeField] private Button exitButton;
    [Header("==== UI Elements ====")]
    [Space(10)]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanelGameObject;
    [SerializeField] public GameObject grayOutPanel;
    [SerializeField] public GameObject mainMenuImage;
    [SerializeField] public GameObject exitPanelGameObject;
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private SettingsPanelController settingsPanelController;
    [SerializeField] private AudioPanel audioPanel;
    [SerializeField] private GraphicPanel graphicPanel;
    [SerializeField] private ResulationPanel resulationPanel;
    [SerializeField] private ControlsPanel controlsPanel;
    [SerializeField] private Credits creditsPanel;
    [SerializeField] private ExitPanel exitPanel;
    [Header("==== Anothers ====")]
    [Space(10)]
    [SerializeField] private string nextSceneName;
    private int currentIndex = 0;
    private bool inSettingsPanel = false;
    public bool inMainMenu = false;
    private bool isMouseOverButton = false;

    private void Start()
    {
        grayOutPanel.SetActive(false);
        settingsPanelController = settingsPanel.GetComponent<SettingsPanelController>();
        inMainMenu = true;
    }
    public void Update()
    {
        HandleMainMenuPanelInput();
        SetButtonToFirst();
    }
    public void SetButtonToFirst()
    {
        if (inMainMenu)
        {
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
            ToggleAttachedObject(buttons[currentIndex], true);
        }
    }
    public void HandleMainMenuPanelInput()
    {
        if (!exitPanel.inExitPanel && !settingsPanelController.inSettingsPanel && !audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel && !creditsPanel.inCreditsPanel)
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
    private void Submit()
    {
        Button selectedButton = buttons[currentIndex];
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (selectedButton == playButton)
            {
                playButton.onClick.Invoke();
            }
            else if (selectedButton == settingsButton)
            {
                settingsButton.onClick.Invoke();
            }
            else if (selectedButton == extrasButton)
            {
                extrasButton.onClick.Invoke();
            }
            else if (selectedButton == exitButton)
            {
                exitButton.onClick.Invoke();
            }
        }
    }
    private void ToggleAttachedObject(Button button, bool state)
    {
        GameObject attachedObject = button.gameObject.transform.GetChild(0).gameObject;
        attachedObject.SetActive(state);
    }
    public void Play()
    {
        if (!exitPanel.inExitPanel && !settingsPanelController.inSettingsPanel && !audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel && !creditsPanel.inCreditsPanel)
        {
            AudioManager.Instance.PlaySFX("InterfaceGO");
            SceneManager.LoadScene(nextSceneName);
            Debug.Log("Play Level, next scene");
        }
        else
        {
            Debug.Log("!");
        }

    }
    public void Settings()
    {
        if (!exitPanel.inExitPanel && !settingsPanelController.inSettingsPanel && !audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel && !creditsPanel.inCreditsPanel)
        {
            AudioManager.Instance.PlaySFX("InterfaceGO");
            mainMenuImage.SetActive(false);
            settingsPanel.SetActive(true);
            settingsPanelController.inSettingsPanel = true;
            inMainMenu = false;
            grayOutPanel.SetActive(true);

            

        }
        if (!inMainMenu)
        {
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        if (settingsPanel.activeSelf == true)
        {
            currentIndex = 0;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }

    }
    public void Extras()
    {
        if (!exitPanel.inExitPanel && !settingsPanelController.inSettingsPanel && !audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel && !creditsPanel.inCreditsPanel)
        {
            AudioManager.Instance.PlaySFX("InterfaceGO");
            creditsPanelGameObject.SetActive(true);
            creditsPanel.inCreditsPanel = true;
            inMainMenu = false;
        }
        if (!inMainMenu)
        {
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        if (creditsPanelGameObject.activeSelf == true)
        {
            currentIndex = 0;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
    }

    public void Exit()
    {
        if (!exitPanel.inExitPanel && !settingsPanelController.inSettingsPanel && !audioPanel.inAudioPanel && !graphicPanel.inGraphicPanel && !resulationPanel.resulationPanelIsActive && !controlsPanel.inControlPanel && !creditsPanel.inCreditsPanel)
        {
            AudioManager.Instance.PlaySFX("InterfaceGO");
            exitPanelGameObject.SetActive(true);
            exitPanel.inExitPanel = true;
            grayOutPanel.SetActive(true);
            inMainMenu = false;
            mainMenuImage.SetActive(false);
        }
        if (!inMainMenu)
        {
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        if (exitPanelGameObject.activeSelf == true)
        {
            currentIndex = 0;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
    }
}
