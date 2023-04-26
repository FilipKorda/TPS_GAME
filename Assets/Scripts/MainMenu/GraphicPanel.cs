using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GraphicPanel : MonoBehaviour
{
    [Header("==== Lista Buttonów ====")]
    [Space(10)]
    [SerializeField] public Button[] buttons;
    [Header("==== Buttony ====")]
    [Space(10)]
    [SerializeField] public Button resolutionOneButton;
    [SerializeField] public Button resolutionTwoButton;
    [SerializeField] public Button resolutionThreeButton;
    [SerializeField] public Button lowQualityButton;
    [SerializeField] public Button mediumQualityButton;
    [SerializeField] public Button highQualityButton;
    [SerializeField] public Button antiAlias2xButton;
    [SerializeField] public Button antiAlias4xButton;
    [SerializeField] public Button antiAlias8xButton;
    [SerializeField] public Button fullscreen;
    [SerializeField] public Button vsync;
    [SerializeField] public Button antiAliasing;
    [SerializeField] public Button backButton;
    [Header("==== UI Elements ====")]
    [Space(10)]
    [SerializeField] private GameObject graphicPanel;
    [SerializeField] private GameObject acceptResolutionPanel;
    [SerializeField] private GameObject settingsImage;
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private SettingsPanelController settingsPanelController;
    [SerializeField] private PauseMenu pasueMenu;
    [SerializeField] private ResulationPanel resulationPanel;
    [Header("==== Anothers ====")]
    [Space(10)]
    public bool inGraphicPanel = false;
    private int currentIndex = 0;
    private bool isMouseOverButton = false;
    private void Update()
    {
        HandleGraphicPanelInput();
        SetButtonToFirstS();
    }

    public void SetButtonToFirstS()
    {
        if (inGraphicPanel)
        {
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
            ToggleAttachedObject(buttons[currentIndex], true);
        }
    }
    private void HandleGraphicPanelInput()
    {
        if (!resulationPanel.resulationPanelIsActive)
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

    }
    private void Submit()
    {
        Button selectedButton = buttons[currentIndex];
        if (Input.GetKeyDown(KeyCode.X))
        {            
            if (selectedButton == resolutionOneButton)
            {
                resolutionOneButton.onClick.Invoke();
            }
            else if (selectedButton == resolutionTwoButton)
            {
                resolutionTwoButton.onClick.Invoke();
            }
            else if (selectedButton == resolutionThreeButton)
            {
                resolutionThreeButton.onClick.Invoke();
            }
            else if (selectedButton == lowQualityButton)
            {
                lowQualityButton.onClick.Invoke();
            }
            else if (selectedButton == mediumQualityButton)
            {
                mediumQualityButton.onClick.Invoke();
            }
            else if (selectedButton == highQualityButton)
            {
                highQualityButton.onClick.Invoke();
            }
            else if (selectedButton == antiAlias2xButton)
            {
                antiAlias2xButton.onClick.Invoke();
            }
            else if (selectedButton == antiAlias4xButton)
            {
                antiAlias4xButton.onClick.Invoke();
            }
            else if (selectedButton == antiAlias8xButton)
            {
                antiAlias8xButton.onClick.Invoke();
            }
            else if (selectedButton == fullscreen)
            {
                fullscreen.onClick.Invoke();
            }
            else if (selectedButton == vsync)
            {
                vsync.onClick.Invoke();
            }
            else if (selectedButton == antiAliasing)
            {
                antiAliasing.onClick.Invoke();
            }
            else if (selectedButton == backButton)
            {
                backButton.onClick.Invoke();
            }
        }

    }
    public void ToggleAttachedObject(Button button, bool state)
    {
        GameObject attachedObject = button.gameObject.transform.GetChild(0).gameObject;
        attachedObject.SetActive(state);
    }
    public void OnBackButtonClick()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            settingsImage.SetActive(true);
            settingsPanelController.grayOutPanel.SetActive(false);
            graphicPanel.SetActive(false);
            inGraphicPanel = false;
            settingsPanelController.inSettingsPanel = true;
            if (!inGraphicPanel)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            if (graphicPanel.activeSelf == false)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], false);
            }
        }
        if (SceneManager.GetActiveScene().name == "LEVEL")
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            pasueMenu.grayOutPanel.SetActive(false);
            inGraphicPanel = false;
            graphicPanel.SetActive(false);
            pasueMenu.inPauseMenu = true;
            if (!inGraphicPanel)
            {
                ToggleAttachedObject(buttons[currentIndex], false);
            }
            if (graphicPanel.activeSelf == false)
            {
                currentIndex = 0;
                ToggleAttachedObject(buttons[currentIndex], false);
            }
        }
    }
    public void OnResolutionOneButtonClick()
    {
        AudioManager.Instance.PlaySFX("InterfaceGO");
        Screen.SetResolution(1280, 720, Screen.fullScreen);
        acceptResolutionPanel.SetActive(true);
        inGraphicPanel = false;
        resulationPanel.resulationPanelIsActive = true;

        ToggleAttachedObject(buttons[currentIndex], false);
        if (graphicPanel.activeSelf == true)
        {
            currentIndex = 3;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void OnResolutionTwoButtonClick()
    {
        AudioManager.Instance.PlaySFX("InterfaceGO");
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        acceptResolutionPanel.SetActive(true);
        inGraphicPanel = false;
        resulationPanel.resulationPanelIsActive = true;

        ToggleAttachedObject(buttons[currentIndex], false);
        if (graphicPanel.activeSelf == true)
        {
            currentIndex = 3;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void OnResolutionThreeButtonClick()
    {
        AudioManager.Instance.PlaySFX("InterfaceGO");
        Screen.SetResolution(2560, 1440, 0);
        acceptResolutionPanel.SetActive(true);
        inGraphicPanel = false;
        resulationPanel.resulationPanelIsActive = true;

        ToggleAttachedObject(buttons[currentIndex], false);
        if (graphicPanel.activeSelf == true)
        {
            currentIndex = 3;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
    }
    //TO DO
    public void OnFullscreenButtonClick()
    {
        AudioManager.Instance.PlaySFX("ApplyButton");
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("FullScreen");
    }
    public void OnVsyncButtonClick()
    {
        AudioManager.Instance.PlaySFX("ApplyButton");
        QualitySettings.vSyncCount = QualitySettings.vSyncCount == 0 ? 1 : 0;
        Debug.Log("V-SYNC");
    }
    public void OnAntiAliasingButtonClick()
    {
        AudioManager.Instance.PlaySFX("ApplyButton");
        QualitySettings.antiAliasing = QualitySettings.antiAliasing == 0 ? 1 : 0;
        Debug.Log("AntiAliasing");
    }
    public void SetQualityButtonClick(int qualityIndex)
    {
        AudioManager.Instance.PlaySFX("ApplyButton");
        QualitySettings.SetQualityLevel(qualityIndex);
        acceptResolutionPanel.SetActive(true);
        inGraphicPanel = false;
        resulationPanel.resulationPanelIsActive = true;
        ToggleAttachedObject(buttons[currentIndex], false);
        if (graphicPanel.activeSelf == true)
        {
            currentIndex = 3;
            ToggleAttachedObject(buttons[currentIndex], false);
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void SetAntiAlias(int alias)
    {
        AudioManager.Instance.PlaySFX("ApplyButton");
        QualitySettings.antiAliasing = alias;
        Debug.Log("antiAlias");
    }

}
